using Mediator;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Caching
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : ICacheable
         where TResponse : class        
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<CachingBehaviour<TRequest, TResponse>> logger;

        public CachingBehaviour(IMemoryCache cache, ILogger<CachingBehaviour<TRequest, TResponse>> logger)
        {
            this.cache = cache;
            this.logger = logger;
        }
        public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            var requestName = message.GetType();
            logger.LogInformation("{Request} is configured for caching.", requestName);

            TResponse response;
            if (message.NeedCache && cache.TryGetValue(message.CacheKey, out response))
            {
                logger.LogInformation("Returning cached value for {Request}.", requestName);
                return response;
            }

            logger.LogInformation("{Request} Cache Key: {Key} is not inside the cache, executing request.", requestName, message.CacheKey);
            response = await next(message,cancellationToken);
            cache.Set(message.CacheKey, response);
            return response;
        }
    }
}
