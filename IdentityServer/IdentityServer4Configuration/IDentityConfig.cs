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
                    Name =IDentityConstants.ApiResource,
                    DisplayName = IDentityConstants.ApiResource,
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
                          IDentityConstants.AngularApiScope
                    },
                    ApiSecrets = new List<Secret>(){new Secret(IDentityConstants.ApiResourceSecret.Sha256()) },
                },
                 new ApiResource
                 {
                    Name = IDentityConstants.MVCResource,
                    DisplayName = IDentityConstants.MVCResource,
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
                    ApiSecrets = new List<Secret>(){new Secret(IDentityConstants.MVCResourceSecret.Sha256()) },
                 }

           };

        internal static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope(IDentityConstants.AngularApiScope),
            };


        internal static IEnumerable<Client> ApiClients =>
            new List<Client>()
            {
                new Client
                {

                        ClientId = IDentityConstants.MobileClientSecret,
                        ClientSecrets = { new Secret(IDentityConstants.MobileClientSecret.Sha256()) },
                        AllowOfflineAccess = true,
                        AccessTokenLifetime = IDentityConstants.TokenLifeTime,
                        UpdateAccessTokenClaimsOnRefresh =true,
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        RefreshTokenExpiration = TokenExpiration.Absolute,
                        AbsoluteRefreshTokenLifetime =  IDentityConstants.RefreshTokenLifeTime,
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
                      ClientId = IDentityConstants.AngularClientId,
                      ClientName = IDentityConstants.AngularClientName,
                      ClientSecrets = { new Secret(IDentityConstants.AngularClientSecret.Sha256()) },
                      AllowOfflineAccess = true,
                      AccessTokenLifetime = IDentityConstants.TokenLifeTime,
                      UpdateAccessTokenClaimsOnRefresh =true,
                      AllowedGrantTypes = GrantTypes.Code,
                      RefreshTokenExpiration = TokenExpiration.Absolute,
                      AbsoluteRefreshTokenLifetime =  IDentityConstants.RefreshTokenLifeTime ,
                      AllowedScopes =
                      {
                          IdentityServerConstants.LocalApi.ScopeName,
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile,
                          IDentityConstants.AngularApiScope,
                          IdentityServerConstants.StandardScopes.OfflineAccess,

                      },
                      RefreshTokenUsage= TokenUsage.OneTimeOnly,
                      RedirectUris = IDentityAppSettings.AngularClient_RedirectUris,
                      RequirePkce = true,
                      AllowAccessTokensViaBrowser = true,
                      AllowedCorsOrigins = IDentityAppSettings.AngularClient_CORS,
                      RequireClientSecret = false,
                      PostLogoutRedirectUris =IDentityAppSettings.AngularClient_PostLogoutRedirectUris,
                      RequireConsent = false,
                },

            };
    }
}
