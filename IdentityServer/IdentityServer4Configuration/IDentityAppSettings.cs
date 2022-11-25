using IdentityServer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer4Configuration
{
    public class IDentityAppSettings
    {

        public static string AllowCors => "ClientAllowed";

        //--------------Angular Client URLS----------------------------
        public static ICollection<string> AngularClient_RedirectUris =>
            ConfigurationHelper.StaticConfigurations.GetValue<string>("Angular:RedirectUris").Split(",");
        public static ICollection<string> AngularClient_CORS =>
            ConfigurationHelper.StaticConfigurations.GetValue<string>("Angular:CORS").Split(",");
        public static ICollection<string> AngularClient_PostLogoutRedirectUris =>
            ConfigurationHelper.StaticConfigurations.GetValue<string>("Angular:PostLogoutRedirectUris").Split(",");







    }
}
