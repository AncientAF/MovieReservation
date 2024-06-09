using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Shared.Caching
{
    public class DefaultCacheService : ICacheService
    {
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<DefaultCacheService> _logger;

        public DefaultCacheService(IDistributedCache distributedCache, ILogger<DefaultCacheService> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }


        public async Task<T> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var cachedResult = await _distributedCache.GetStringAsync(key, cancellationToken);
            
            //Если ответ есть в кэше отправить ответ
            if (!string.IsNullOrEmpty(cachedResult))
            {
                _logger.LogInformation("Retrieved cache with key: {Key} and expiration: {Expiration}", key, expiration ?? DefaultExpiration);
                return JsonSerializer.Deserialize<T>(cachedResult)!;
            }
            
            //Результат выполнения запроса для дальнейшего кэширования
            var result = await factory(cancellationToken);
            
            DistributedCacheEntryOptions options = new()
            {
                AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
            };
            
            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(result), options, cancellationToken);

            _logger.LogInformation("Cached request with key: {Key} and expiration: {Expiration}", key, expiration ?? DefaultExpiration);
            
            return result;
        }
        public async Task DeleteAsync(string key, CancellationToken cancellationToken)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
