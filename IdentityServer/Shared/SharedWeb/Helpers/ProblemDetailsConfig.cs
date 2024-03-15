using Microsoft.Extensions.DependencyInjection;

namespace SharedWeb.Helpers;
public static class ProblemDetailsConfig
{
    public static IServiceCollection AddApiProblemDetails(this IServiceCollection services) 
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = (context) =>
            {

                context.ProblemDetails.Extensions["ServerVersion"] = "asdasdas";
                context.ProblemDetails.Extensions["ClientVersion"] = "asdasdas";
            };
        });

        return services;
    }
}