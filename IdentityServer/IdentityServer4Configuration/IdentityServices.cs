using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedData;
using SharedData;
using SharedLogic.IdentityServer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer4Configuration
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServerV4(this IServiceCollection services,
                                                             IConfiguration configuration,
                                                             IWebHostEnvironment env)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();


            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;


            var migrationsAssembly = typeof(IdentityContext).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString("Default");
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            }).AddInMemoryClients(IDentityConfig.ApiClients)
                .AddInMemoryIdentityResources(IDentityConfig.IdentityResources)
                .AddInMemoryApiScopes(IDentityConfig.ApiScopes)
                .AddInMemoryApiResources(IDentityConfig.ApiResources)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = IDentityConstants.TokenCleanupInterval;
                })
               .AddAspNetIdentity<User>()
               .AddResourceOwnerValidator<ResourceOwnerValidator>()
               .AddProfileService<Profile>();

            #region Certifcate


            AddSigninCredenticals(services, configuration, env, builder);
            #endregion

            ConfigureAuthorityOfIDSasClientCookieAndJWT(services, configuration);
            services.AddLocalApiAuthentication();

            ConfigureAPIReturnsUnAuthorize(services);

            return services;
        }

        private static void AddSigninCredenticals(
            IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env,
            IIdentityServerBuilder builder)
        {
            if (env.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                X509Certificate2 cert = null;
                var certificateThumbprint = configuration["CertificateThumbprint"];
                using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
                {
                    store.Open(OpenFlags.MaxAllowed);
                    var certs = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);

                    if (certs.Count > 0)
                    {
                        try
                        {
                            //check if system user can read cert private key
                            if (certs[0].PrivateKey != null)
                            {
                                cert = certs[0];
                            }
                        }
                        catch (Exception)
                        {
                            //fall back to read cert from file
                        }
                    }
                    store.Close();
                }
                if (cert == null)
                {
                    var filePath = Path.Combine(AppContext.BaseDirectory, "identity.pfx");
                    cert = new X509Certificate2(filePath, "identity", X509KeyStorageFlags.MachineKeySet);

                }

                services.AddDataProtection()
                        .SetApplicationName("identity")
                        .PersistKeysToFileSystem(new DirectoryInfo($@"{Path.GetFullPath(Path.Combine(env.ContentRootPath, @"..\keys"))}"));
                if (cert == null)
                    throw new Exception("need to configure key material");
                else
                    builder.AddSigningCredential(cert)
                           .AddValidationKey(cert);


            }
        }

        private static void ConfigureAPIReturnsUnAuthorize(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.LogoutPath = new PathString("/Account/Logout");

                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api")
                        && context.Response.StatusCode == StatusCodes.Status200OK)
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });
        }

        private static void ConfigureAuthorityOfIDSasClientCookieAndJWT(IServiceCollection services, IConfiguration configuration)
        {
            var authorityOfMySelf = configuration.GetValue<string>("Authority");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.SlidingExpiration = true;
             })
             .AddJwtBearer(options =>
             {

                 options.Authority = authorityOfMySelf;
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;

                 options.TokenValidationParameters =
                 new TokenValidationParameters
                 {
                     LifetimeValidator = (_, expires, __, ___) => expires > DateTime.UtcNow,
                     ValidateAudience = false,
                     ValidateIssuer = true,
                     ValidateActor = false,
                     ValidateLifetime = true,
                     NameClaimType = "name",
                     RoleClaimType = "role"
                 };
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];

                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) &&
                             (path.StartsWithSegments("/hubs")))
                         {
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     }
                 };
             });
        }
    }
}
