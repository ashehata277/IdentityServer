using IdentityServer.Helper;

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
        //------------------------------swagger -------------------------------------------------
        public static string Authority =>
           ConfigurationHelper.StaticConfigurations.GetValue<string>("Authority");


        public static string SwaggerFrontChannelLogoutUri =>
          ConfigurationHelper.StaticConfigurations.GetValue<string>("Swagger:FrontChannelLogoutUri");
        public static ICollection<string> SwaggerPostLogoutRedirectUris =>
         ConfigurationHelper.StaticConfigurations.GetValue<string>("Swagger:PostLogoutRedirectUris").Split(",");
        public static ICollection<string> SwaggerRedirectUris =>
         ConfigurationHelper.StaticConfigurations.GetValue<string>("Swagger:RedirectUris").Split(",");







    }
}
