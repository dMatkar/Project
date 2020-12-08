namespace Project.Core.Caching
{
    /// <summary>
    /// Used when we want remove caching.
    /// </summary>
    public class NullCacheManager : ICacheManager
    {
        public void Clear()
        {
        }

        public T Get<T>(string key)
        {
            return default;
        }

        public bool IsSet(string key)
        {
            return false;
        }

        public void Remove(string key)
        {
        }

        public void RemoveByPattern(string pattern)
        {
        }

        public bool Set(string key, object data, int cacheTime)
        {
            return false;
        }
    }
}
