namespace Shared.Caching
{
    public interface ICacheService
    {
        Task DeleteAsync(string key, CancellationToken cancellationToken);
        Task<T> GetOrCreateAsync<T>(
            string key, Func<CancellationToken,
            Task<T>> factory,
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default);
    }
}
