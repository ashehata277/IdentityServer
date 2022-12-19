using Microsoft.AspNetCore.Builder;
using NSwag.AspNetCore;

namespace SharedWeb.Helpers
{
    public static class CustomMiddlewares
    {
        public static void UseAuthSwagger(this WebApplication app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "5754f3b1-38ef-4f2f-9ee6-7b9d36066af2",
                    AppName = "swagger"
                };
            });
        }
    }
}
