using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.IdentityServer
{
    public class IDentityConstants
    {
        public static readonly int TokenLifeTime = 3600;
        public static readonly int RefreshTokenLifeTime = 2592000;
        public static readonly int TokenCleanupInterval = 2592000;
        public static readonly string ApiResource = "Api";
        public static readonly string MVCResource = "MVC";
        public static readonly string AdminUserName = "admin";
        public static readonly string AdminPassword = "admin";
        public static readonly string AdminUserId = "32efec81-d676-4e7a-b3c1-12984c515bf2";
        public static readonly string AdminRoleName = "admin";
        public static readonly string AdminRoleId = "8ce88938-6386-40ce-ab79-16306e3991a8";
        public static readonly string AdminPhoneNumber = "admin";
        public static readonly string AdminPhoneEmail = "admin@admin.com";
        public static readonly string TokenInfo_Name = "username";
        public static readonly string TokenInfo_AuthenticationMethod = "custom";
        public static readonly string TokenInfo_IdentityProvider = "local";
        public static readonly string SecurityStamp = "SecurityStamp";


        //-------------------Claims--------------------------------
        public static readonly string RoleClaim = "role";
        public static readonly string UserNameClaim = "username";
        public static readonly string NameClaim = "name";
        public static readonly string UserIdClaim = "sub";
        public static readonly string ClientTypeClaim = "ClientType";


        //-------------------- Scopes---------------------------------------------
        public static readonly string AngularApiScope = "Angular-Client-Scope";
        public static readonly string SwaggerScope = "swagger-Client-Scope";

        //-------------------- mobile Client ----------------------------------------
        public static readonly string MobileClientSecret = "83906b4d-72b6-4acf-9a6d-cdcf546c3f33";
        public static readonly string MobileClientId = "bea8cbfe-188e-4d5b-ad50-f2c2df46971d";
        public static readonly string ApiResourceSecret = "db62d929-8f78-4c80-b9dd-8d9c9ec86e77";
        public static readonly string MVCResourceSecret = "ccba8b8c-d866-4859-87ef-e36ca70aa86c";



        //----------------------Angular client -----------------------------------
        public static readonly string AngularClientId = "8a2fd0e6-4c13-4581-84f0-e279ec81a2c5";
        public static readonly string AngularClientName = "Angular-Client";
        public static readonly string AngularClientSecret = "a2068121-2c4a-479a-9cc2-4b9d0dc11efd";
        public static readonly string AngularClientType = "Angular";


        //--------------------------swagger client ---------------------------------------------
        public static readonly string SwaggerClientId = "5754f3b1-38ef-4f2f-9ee6-7b9d36066af2";
        public static readonly string SwaggerClientSecret = "e4ca12d6-57ee-4a57-826b-f07bda32c879";
        public static readonly string SwaggerClientName = "swagger";






    }
}
