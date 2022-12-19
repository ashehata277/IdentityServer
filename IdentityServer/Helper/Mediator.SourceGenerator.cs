using Mediator;
using SharedApplication.Mediator.Common;
using SharedApplication.Mediator.Common.PipelineBehaviours.Caching;
using SharedApplication.Mediator.Common.PipelineBehaviours.Logging;
using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;

namespace IdentityServer.Helper
{
public static class MediatorConfiguration
{
    public static IServiceCollection AddMediatorSourceGenerator(this IServiceCollection services)
    {
        services.AddApplicationMediator();
        return services;
    }
}
}