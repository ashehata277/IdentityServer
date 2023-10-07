using Hellang.Middleware.ProblemDetails;
using IdentityServer.DataBaseConfiguration;
using IdentityServer.Helper;
using IdentityServer.IdentityServer4Configuration;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using SharedWeb.Helpers;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.WebHost.UseSerilog((provider, loggerConfig) =>
    {
        loggerConfig.ReadFrom
        .Configuration(configuration)
        .WriteTo
        .Console();

    }); 


string allowedOrigins = IDentityAppSettings.AllowCors;
IWebHostEnvironment env = builder.Environment;
builder.Services
    .InitializeConfiguration(configuration)
    .AddMediatorSourceGenerator()
    .AddIDentityDataBaseConfiguration(configuration)
    .AddIdentityServerV4(configuration, env)
    .AddServiers()
    .AddAuthSwagger()
    .AddCORS(configuration, allowedOrigins)
    .AddLocalization()
    .AddApiProblemDetails()
    .AddControllersWithViews()
    .AddNewtonsoftJson();


var app = builder.Build();

app.MigrateContexts();
app.UseProblemDetails();
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
