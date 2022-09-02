using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace PostgresMarten.Extensions
{
    public static class CacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string cacheKey,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            var encodedData = Encoding.UTF8.GetBytes(jsonData);
            await cache.SetAsync(cacheKey, encodedData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string cacheKey)
        {
            var cacheData = await cache.GetAsync(cacheKey);

            if (cacheData is null)
            {
                return default(T);
            }
            var decodedData = Encoding.UTF8.GetString(cacheData);
            return JsonSerializer.Deserialize<T>(decodedData);
        }
    }
}
