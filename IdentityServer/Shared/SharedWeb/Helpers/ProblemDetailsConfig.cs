using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SharedWeb.Helpers;
using System;

namespace IdentityServer.Helper;
public static class ProblemDetailsConfig
{
    public static IServiceCollection AddApiProblemDetails(this IServiceCollection services) 
    {
        services.AddProblemDetails();

        return services;
    }
}