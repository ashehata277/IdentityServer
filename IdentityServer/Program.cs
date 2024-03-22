using IdentityServer.DataBaseConfiguration;
using IdentityServer.Helper;
using IdentityServer.IdentityServer4Configuration;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using SharedWeb.Helpers;

var builder = WebApplication.CreateBuilder(args);
//develop
ConfigurationManager configuration = builder.Configuration;
builder.WebHost.UseSerilog((provider, loggerConfig) =>
    {
        loggerConfig.ReadFrom
        .Configuration(configuration)
        .WriteTo
        .Console();

    }); 


string allowedOrigins = IdentityAppSettings.AllowCors;
IWebHostEnvironment env = builder.Environment;
builder.Services
    .InitializeConfiguration(configuration)
    .AddMediatorSourceGenerator()
    .AddIdentityDataBaseConfiguration(configuration)
    .AddIdentityServerV4(configuration, env)
    .AddAuthorizationHandlers()
    .AddServices()
    .AddAuthSwagger()
    .AddC_O_R_S(configuration, allowedOrigins)
    .AddLocalization()
    .AddApiProblemDetails()
    .AddControllersWithViews()
    .AddNewtonsoftJson();




var app = builder.Build();

app.MigrateContexts();
app.UseAuthSwagger();
if (!WindowsServiceHelpers.IsWindowsService())
    app.UseHttpsRedirection();
app.UseAdminUser();
app.UseStaticFiles();
app.UseCors(allowedOrigins);
app.UseLocalization();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}",
        defaults: new { controller = "Home",Action = "Index"});
});
app.Run();
