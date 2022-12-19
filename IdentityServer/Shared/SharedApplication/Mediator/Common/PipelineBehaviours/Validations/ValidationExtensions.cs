using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public static class ValidationExtensions
    {
        public static void AddValidatorsFromExceutingAssembly(this IServiceCollection services)
        {
            services.Scan(scan => scan
              .FromAssemblyOf<IValidationHandler>()
                .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
                  .AsImplementedInterfaces()
                  .WithTransientLifetime());

        }


        public static void AddValidators(this IServiceCollection services, Assembly assembly)
        {
            services.Scan(scan => scan
              .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
                  .AsImplementedInterfaces()
                  .WithTransientLifetime());
        }
    }
}
