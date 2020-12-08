namespace Project.Core.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// Get the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Set the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        bool Set(string key, object data, int cacheTime);

        /// <summary>
        /// Cache for perticular key is set or not.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSet(string key);

        /// <summary>
        /// Remove cache by key.
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// Remove cached data.
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Clear all cache.
        /// </summary>
        void Clear();
    }
}
