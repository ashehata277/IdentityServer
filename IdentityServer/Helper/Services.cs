using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Helper
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();

            return services;
        }
    }
}
