using IdentityServer.DataBaseConfiguration;
using IdentityServer.IdentityServer4Configuration;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
Log.Logger = new LoggerConfiguration()
                          .ReadFrom.Configuration(configuration)
                          .WriteTo.Console()
                          .CreateLogger();



builder.Services.AddControllers();
builder.Services.AddIDentityDataBaseConfiguration(configuration)
    .AddIdentityServerV4(configuration);


var app = builder.Build();

app.MigrateContexts();


if (!WindowsServiceHelpers.IsWindowsService())
    app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseRouting();
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
