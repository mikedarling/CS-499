using System;
using System.Runtime.Caching;

namespace AnimalRescue.Caching.Providers
{
    public class MemoryCacheProvider : ICacheProvider
    {

        #region Local Variables

        private static readonly CacheItemPolicy _cacheItemPolicy = new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromMinutes(20.0) };

        private static readonly MemoryCache _cache = MemoryCache.Default;

        #endregion

        #region Methods

        public T Add<T>(string key, T obj)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (obj == null && _cache.Contains(key))
            {
                this.Remove(key);
                return default(T);
            }

            _cache.Set(key, obj, _cacheItemPolicy);
            return obj;
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!_cache.Contains(key))
            {
                return default(T);
            }

            var obj = _cache.Get(key);

            if (obj == null || !(obj is T))
            {
                return default(T);
            }

            return (T)obj;
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!_cache.Contains(key))
            {
                return true;
            }

            _cache.Remove(key);
            return true;
        }

        #endregion

    }
}
