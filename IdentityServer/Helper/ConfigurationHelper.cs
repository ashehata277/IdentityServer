using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Helper
{
    public static class ConfigurationHelper
    {
        public static IConfiguration? StaticConfigurations { get; set; }
        public static IServiceCollection InitializeConfiguration(this IServiceCollection services,IConfiguration configuration) 
        {
            StaticConfigurations = configuration;
            return services;
        }
    }
}
