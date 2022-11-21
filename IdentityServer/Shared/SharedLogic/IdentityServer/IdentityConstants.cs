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
        public static readonly string MobileClientSecret= "83906b4d-72b6-4acf-9a6d-cdcf546c3f33";
        public static readonly string MobileClientId= "bea8cbfe-188e-4d5b-ad50-f2c2df46971d";
        public static readonly string ApiResourceSecret= "db62d929-8f78-4c80-b9dd-8d9c9ec86e77";
        public static readonly string ApiResource= "Api";
        public static readonly string MVCResourceSecret= "ccba8b8c-d866-4859-87ef-e36ca70aa86c";
        public static readonly string MVCResource= "MVC";
        public static readonly int TokenCleanupInterval = 2592000;
    }
}
