using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace EventApi.Handlers
{
    public class CacheHandler<T>(IMemoryCache cache) : ICacheHandler<T>
    {
        private readonly IMemoryCache _cache = cache;
        public T? GetFromCache(string cachkey)
        {
            if (_cache.TryGetValue(cachkey, out T? cachedData))
            {
                return cachedData;
            }
            return default;
        }
        public T SetCache (string cachkey, T data, int expirationTime = 10)
        {
            _cache.Remove(cachkey);
            _cache.Set(cachkey, data, TimeSpan.FromMinutes (expirationTime));
            return data;
        }
    }

    public interface ICacheHandler<T>
    {
        T? GetFromCache(string cachkey);
        T SetCache(string cachkey, T data, int expirationTime = 10);
    }
}
