using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Helper
{
    public static class CorsHelper
    {
        public static IServiceCollection AddC_O_R_S(this IServiceCollection services, IConfiguration configuration,string allowedOrigin) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: allowedOrigin,
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
