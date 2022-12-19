using Mediator;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication.Mediator.Common.PipelineBehaviours.Caching;
using SharedApplication.Mediator.Common.PipelineBehaviours.Logging;
using SharedApplication.Mediator.Common.PipelineBehaviours.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common
{
    public static class AddMediatorServiceCollection
    {
        public static void AddApplicationMediator(this IServiceCollection services) 
        {
            services.AddMediator(opt =>
            {
                opt.ServiceLifetime = ServiceLifetime.Scoped;
            });
            services.AddValidatorsFromExceutingAssembly();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
        }
    }
}
