using IdentityServer.IdentityServer4Configuration;
using IdentityServer4;
using NSwag;
using NSwag.Generation.Processors.Security;
using SharedLogic.IdentityServer;

namespace IdentityServer.Helper
{
    internal static class OAuth2SwaggerConfiguration
    {
        public static IServiceCollection AddAuthSwagger(this IServiceCollection services)
        {
            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {

                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "My Authentication",
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { IdentityConstants.SwaggerScope,IdentityConstants.SwaggerScope},
                                { IdentityServerConstants.LocalApi.ScopeName,IdentityServerConstants.LocalApi.ScopeName},

                            },
                            AuthorizationUrl = $"{IdentityAppSettings.Authority}/connect/authorize",
                            TokenUrl = $"{IdentityAppSettings.Authority}/core/connect/token"
                        },
                    }

                });

                document.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("bearer"));
                document.AllowReferencesWithProperties = true;
            });
            return services;
        }
    }
}
