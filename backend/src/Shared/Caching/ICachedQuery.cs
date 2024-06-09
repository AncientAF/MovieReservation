using Shared.CQRS;

namespace Shared.Caching
{
    public interface ICachedQuery
    {
        string Key { get; }
        TimeSpan? Expiration { get; }
    }

    public interface ICachedQuery<out TResponse> : IQuery<TResponse>, ICachedQuery where TResponse : notnull;
}
