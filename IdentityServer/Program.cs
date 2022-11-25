using IdentityServer.DataBaseConfiguration;
using IdentityServer.Helper;
using IdentityServer.IdentityServer4Configuration;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
Log.Logger = new LoggerConfiguration()
                          .ReadFrom.Configuration(configuration)
                          .WriteTo.Console()
                          .CreateLogger();


string AllowedOrigins = IDentityAppSettings.AllowCors;
IWebHostEnvironment env = builder.Environment;
builder.Services
    .InitializeConfiguration(configuration)
    .AddIDentityDataBaseConfiguration(configuration)
    .AddIdentityServerV4(configuration, env)
    .AddServiers()
    .AddCORS(configuration,AllowedOrigins)
    .AddControllers();


var app = builder.Build();

app.MigrateContexts();

if (!WindowsServiceHelpers.IsWindowsService())
    app.UseHttpsRedirection();
app.UseAdminUser();
app.UseStaticFiles();
app.UseCors(AllowedOrigins);
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
