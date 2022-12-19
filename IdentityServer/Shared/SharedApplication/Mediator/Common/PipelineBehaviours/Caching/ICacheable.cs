using Mediator;

namespace SharedApplication.Mediator.Common.PipelineBehaviours.Caching
{
    public interface ICacheable : IMessage
    {
        string CacheKey { get; }
        bool NeedCache { get; }
    }
}
