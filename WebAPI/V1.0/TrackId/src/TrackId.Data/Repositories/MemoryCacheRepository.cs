using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TrackId.Common.Enum;
using TrackId.Data.Interfaces.Repositories;

namespace TrackId.Data.Repositories
{
    public class MemoryCacheRepository : ICacheService
    {
        private static MemoryCache _cache;

        public MemoryCacheRepository()
        {
            Init();
        }

        public T Get<T>(string key) where T : class
        {
            var objectToReturn = _cache.Get(key);
            if (objectToReturn != null)
            {
                if (objectToReturn is T)
                {
                    return objectToReturn as T;
                }

                try
                {
                    return (T)Convert.ChangeType(objectToReturn, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }

            return default(T);
        }

        public async Task<T> Get<T>(string key, Func<Task<T>> getIfNotCached, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class
        {
            var item = Get<T>(key);

            if (item != null)
            {
                return item;
            }

            item = await getIfNotCached();
            InternalSet<T>(key, item, minutesToCache);

            return item;
        }

        public void Set<T>(string key, object data, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class
        {
            var item = Get<T>(key);
            if (item != null)
            {
                Remove<T>(key);
            }

            InternalSet<T>(key, data, minutesToCache);
        }

        public void Update<T>(string key, object data, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class
        {
            var item = Get<T>(key);
            var policy = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes((int)minutesToCache) };

            if (item != null)
            {
                _cache.Remove(key);
                _cache.Set<T>(key, data as T, policy);
            }
            else
            {
                _cache.Set<T>(key, data as T, policy);
            }
        }

        public bool IsSet(string key)
        {
            return _cache.Get(key) != null;
        }

        public void KeepAlive<T>(string key, CacheTimes minutesToCache) where T : class
        {
            var item = Get<T>(key);
            if (item != null && item is T)
            {
                Update<T>(key, item, minutesToCache);
            }
        }

        public void Remove<T>(string key) where T : class
        {
            var item = _cache.Get<T>(key);
            if (item != null)
            {
                _cache.Remove(key);
            }
        }

        public void Clear()
        {
            Init();
        }

        private void Init()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        private void InternalSet<T>(string key, object data, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class
        {
            var policy = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes((int)minutesToCache) };
            if (data != null && data is T)
            {
                _cache.Set<T>(key, data as T, policy);
            }
            else
            {
                _cache.Set(key, new object(), policy);
            }
        }
    }
}