using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Helper
{
    public static class Services
    {
        public static IServiceCollection AddServiers(this IServiceCollection services) 
        {
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            return services;
        }
    }
}
