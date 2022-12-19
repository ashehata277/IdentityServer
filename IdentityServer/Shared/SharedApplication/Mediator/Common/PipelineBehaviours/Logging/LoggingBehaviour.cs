using Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Logging
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IMessage
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            var requestName = message.GetType();
            logger.LogInformation($"{requestName} is starting.");
            var response = await next(message,cancellationToken);
            logger.LogInformation($"{requestName} has finished");
            return response;
        }
    }
}
    