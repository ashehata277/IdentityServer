using Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Validations
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse: IValidationResponse, new ()
        where TRequest : IValidationRequest
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> logger;
        private readonly IValidationHandler<TRequest>? validationHandler;

        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IValidationHandler<TRequest> validationHandler)
        {
            this.logger = logger;
            this.validationHandler = validationHandler;
        }
        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            var requestName = request!.GetType();
            if (validationHandler == null)
            {
                logger.LogInformation("{Request} does not have a validation handler configured.", requestName);
                return await next(request,cancellationToken);
            }

            var result = await validationHandler.Validate(request);
            if (!result.IsSuccess)
            {
                logger.LogWarning("Validation failed for {Request}. Error: {Error}", requestName, result.ErrorMessage);
                return new TResponse { ErrorMessage = result.ErrorMessage };
            }

            logger.LogInformation("Validation successful for {Request}.", requestName);
            return await next(request, cancellationToken);
        }
    }
}
