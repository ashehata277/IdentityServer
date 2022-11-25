using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Helper
{
    public static class CORSHelper
    {
        public static IServiceCollection AddCORS(this IServiceCollection services, IConfiguration configuration,string AllowedOrigin) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedOrigin,
                    builder =>
                    {
                        builder.WithOrigins(configuration.GetValue<string>("CORS").Split(";"))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            return services;
        }
    }
}
