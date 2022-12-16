using IdentityServer.DataBaseConfiguration;
using IdentityServer.Helper;
using IdentityServer.IdentityServer4Configuration;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;

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


string AllowedOrigins = IDentityAppSettings.AllowCors;
IWebHostEnvironment env = builder.Environment;
builder.Services
    .InitializeConfiguration(configuration)
    .AddIDentityDataBaseConfiguration(configuration)
    .AddIdentityServerV4(configuration, env)
    .AddServiers()
    .AddCORS(configuration, AllowedOrigins)
    .AddLocalization()
    .AddControllers();


var app = builder.Build();

app.MigrateContexts();

if (!WindowsServiceHelpers.IsWindowsService())
    app.UseHttpsRedirection();
app.UseAdminUser();
app.UseStaticFiles();
app.UseCors(AllowedOrigins);
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
        defaults: new { controller = "Home", action = "Index" });
});
app.Run();
