using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;

namespace SharedWeb.Helpers;
public static class ProblemDetailsConfig
{
    public static IServiceCollection AddApiProblemDetails(this IServiceCollection services) 
    {
        services.AddProblemDetails(options =>
        {
            
        });

        return services;
    }
}