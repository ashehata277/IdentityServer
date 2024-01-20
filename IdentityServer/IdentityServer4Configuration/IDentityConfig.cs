using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using SharedLogic.IdentityServer;

namespace IdentityServer.IdentityServer4Configuration
{
    public class IDentityConfig
    {
        internal static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        internal static IEnumerable<ApiResource> ApiResources =>
           new List<ApiResource>
           {
                new ApiResource
                {
                    Name =IdentityConstants.ApiResource,
                    DisplayName = IdentityConstants.ApiResource,
                     UserClaims =
                     {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Address,
                        JwtClaimTypes.Confirmation,
                        JwtClaimTypes.EmailVerified,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Profile,
                        JwtClaimTypes.ReferenceTokenId

                     },

                    Scopes=new List<string>
                    {
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.LocalApi.ScopeName,
                          IdentityConstants.AngularApiScope,
                          IdentityConstants.SwaggerScope

                    },
                    ApiSecrets = new List<Secret>(){new Secret(IdentityConstants.ApiResourceSecret.Sha256()) },
                },
                 new ApiResource
                 {
                    Name = IdentityConstants.MVCResource,
                    DisplayName = IdentityConstants.MVCResource,
                     UserClaims =
                     {
                         JwtClaimTypes.Name,
                         JwtClaimTypes.Email,
                         JwtClaimTypes.Subject,
                         JwtClaimTypes.Role,
                         JwtClaimTypes.Address,
                         JwtClaimTypes.Confirmation,
                         JwtClaimTypes.EmailVerified,
                         JwtClaimTypes.Id,
                         JwtClaimTypes.Profile,
                         JwtClaimTypes.ReferenceTokenId

                     },

                    Scopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId
                    },
                    ApiSecrets = new List<Secret>(){new Secret(IdentityConstants.MVCResourceSecret.Sha256()) },
                 }

           };

        internal static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope(IdentityConstants.AngularApiScope),
                new ApiScope(IdentityConstants.SwaggerScope),
            };


        internal static IEnumerable<Client> ApiClients =>
            new List<Client>()
            {
                new Client
                {

                        ClientId = IdentityConstants.MobileClientId,
                        ClientSecrets = { new Secret(IdentityConstants.MobileClientSecret.Sha256()) },
                        AllowOfflineAccess = true,
                        AccessTokenLifetime = IdentityConstants.TokenLifeTime,
                        UpdateAccessTokenClaimsOnRefresh =true,
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        RefreshTokenExpiration = TokenExpiration.Absolute,
                        AbsoluteRefreshTokenLifetime =  IdentityConstants.RefreshTokenLifeTime,
                        AllowedScopes =
                        {
                            IdentityServerConstants.LocalApi.ScopeName,
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.OfflineAccess,

                        },
                        RefreshTokenUsage= TokenUsage.OneTimeOnly,
                },
                new Client
                {
                      ClientId = IdentityConstants.AngularClientId,
                      ClientName = IdentityConstants.AngularClientName,
                      ClientSecrets = { new Secret(IdentityConstants.AngularClientSecret.Sha256()) },
                      AllowOfflineAccess = true,
                      AccessTokenLifetime = IdentityConstants.TokenLifeTime,
                      UpdateAccessTokenClaimsOnRefresh =true,
                      AllowedGrantTypes = GrantTypes.Code,
                      RefreshTokenExpiration = TokenExpiration.Absolute,
                      AbsoluteRefreshTokenLifetime =  IdentityConstants.RefreshTokenLifeTime ,
                      AllowedScopes =
                      {
                          IdentityServerConstants.LocalApi.ScopeName,
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile,
                          IdentityConstants.AngularApiScope,
                          IdentityServerConstants.StandardScopes.OfflineAccess,

                      },
                      RefreshTokenUsage= TokenUsage.OneTimeOnly,
                      RedirectUris = IdentityAppSettings.AngularClientRedirectUris,
                      RequirePkce = true,
                      AllowAccessTokensViaBrowser = true,
                      AllowedCorsOrigins = IdentityAppSettings.AngularClientCors,
                      RequireClientSecret = false,
                      PostLogoutRedirectUris =IdentityAppSettings.AngularClientPostLogoutRedirectUris,
                      RequireConsent = false,
                },
                new Client
                {
                     ClientId = IdentityConstants.SwaggerClientId,
                     ClientName = IdentityConstants.SwaggerClientName,
                     AllowedGrantTypes = GrantTypes.Implicit,
                     RequirePkce = true,
                     ClientSecrets = { new Secret(IdentityConstants.SwaggerClientSecret.Sha256()) },
                     AllowAccessTokensViaBrowser =true,
                     RedirectUris = IdentityAppSettings.SwaggerRedirectUris,
                     FrontChannelLogoutUri = IdentityAppSettings.SwaggerFrontChannelLogoutUri,
                     PostLogoutRedirectUris = IdentityAppSettings.SwaggerPostLogoutRedirectUris,
                     AllowOfflineAccess = true,
                     AllowedScopes =
                     {
                        IdentityConstants.SwaggerScope,
                        IdentityServerConstants.LocalApi.ScopeName
                     }
                },

            };
    }
}
