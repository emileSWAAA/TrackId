using System;
using System.Threading.Tasks;
using TrackId.Common.Enum;

namespace TrackId.Data.Interfaces.Repositories
{
    public interface ICacheService
    {
        T Get<T>(string key) where T : class;

        Task<T> Get<T>(string key, Func<Task<T>> getIfNotCached, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class;

        void Set<T>(string key, object data, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class;

        void Update<T>(string key, object data, CacheTimes minutesToCache = CacheTimes.HalfHour) where T : class;

        bool IsSet(string key);

        void KeepAlive<T>(string key, CacheTimes minutesToCache) where T : class;

        void Remove<T>(string key) where T : class;

        void Clear();
    }
}