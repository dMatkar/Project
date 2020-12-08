using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project.Core.Caching
{
    public static class Extentions
    {
        /// <summary>
        /// Get the cache data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> func, int cacheTime)
        {
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);

            var result = func();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
            return result;
        }

        /// <summary>
        /// Get the cache data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> func)
        {
            return Get(cacheManager, key, func, 30);
        }

        /// <summary>
        /// Remove cache memory by pattern.
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="pattern"></param>
        /// <param name="keys"></param>
        public static void RemoveByPattern(this ICacheManager cacheManager, string pattern, IEnumerable<string> keys)
        {
            if (!keys.Any())
                return;

            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            foreach (var item in keys)
            {
                if (regex.IsMatch(item))
                    cacheManager.Remove(item);
            }
        }
    }
}
