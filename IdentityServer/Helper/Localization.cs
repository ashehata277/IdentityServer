using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace IdentityServer.Helper
{
    public static class Localization
    {
        public static IServiceCollection AddLocalization(IServiceCollection services)
        {
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";

            });
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar")
                };
                opt.DefaultRequestCulture = new RequestCulture("en");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
            });
            return services;
        }

        public static void UseLocalization(this IApplicationBuilder app) 
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }
    }
}
