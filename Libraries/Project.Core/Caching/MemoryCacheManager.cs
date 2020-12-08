using System;
using System.Linq;
using System.Runtime.Caching;

namespace Project.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        #region Properties 

        protected MemoryCache Cache
        {
            get => MemoryCache.Default;
        }

        #endregion

        #region Methods

        public virtual void Clear()
        {
            foreach (var item in Cache)
                Cache.Remove(item.Key);
        }

        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public virtual bool IsSet(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;
            return Cache.Contains(key);
        }

        public virtual void Remove(string key)
        {
            if (IsSet(key))
                Cache.Remove(key);
        }

        public virtual void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, Cache.Select(x => x.Key));
        }

        public virtual bool Set(string key, object data, int cacheTime)
        {
            if (string.IsNullOrWhiteSpace(key) && IsSet(key) && data is null)
                return false;

            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(cacheTime)
            };
            return Cache.Add(new CacheItem(key, data), cacheItemPolicy);
        }

        #endregion
    }
}
