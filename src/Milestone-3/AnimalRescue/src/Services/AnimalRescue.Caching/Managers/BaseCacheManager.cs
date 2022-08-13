using AnimalRescue.Caching.Providers;
using System;
using System.Collections.Generic;

namespace AnimalRescue.Caching
{
    public abstract class BaseCacheManager
    {

        #region Constructor

        public BaseCacheManager(string cachePrefix, ICacheProvider cacheProvider)
        {
            if (string.IsNullOrEmpty(cachePrefix))
            {
                throw new ArgumentNullException(nameof(cachePrefix));
            }

            if (_knownPrefixes.Contains(cachePrefix))
            {
                throw new Exception("Cache Prefix already known");
            }

            this._prefix = cachePrefix;
            this._cacheProvider = cacheProvider;
        }

        #endregion

        #region Local Variables

        private static List<string> _knownPrefixes => new List<string>();

        private readonly string _prefix;

        private readonly ICacheProvider _cacheProvider;

        #endregion

        #region Methods

        public T Add<T>(string key, T obj)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var fullKey = this.GetFullKey(key);

            var cached = this._cacheProvider.Add(fullKey, obj);

            return cached;
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(key);
            }

            var fullKey = this.GetFullKey(key);

            return this._cacheProvider.Get<T>(fullKey);
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(key);
            }

            var fullKey = this.GetFullKey(key);

            return this._cacheProvider.Remove(fullKey);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the full key as the namespace prefix + local key.
        /// </summary>
        /// <param name="key">The key within the namespace.</param>
        /// <returns>The actual cache key.</returns>
        protected string GetFullKey(string key)
        {
            return $"{this._prefix}.{key}";   
        }

        #endregion

    }
}
