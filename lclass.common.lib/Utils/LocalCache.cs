using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Web;
using System.Web.Caching;

namespace lclass.common.lib.Utils
{
    public class LocalCache
    {
        private static ConcurrentDictionary<string, object> LockDictionary = new ConcurrentDictionary<string, object>();
        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //创建缓存依赖项
            CacheDependency dep = new CacheDependency(fileName);
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }

        /// <summary>
        /// 创建缓存项过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="expires">过期时间间隔</param>
        public static void Insert(string key, object obj, TimeSpan expires)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expires);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">T对象</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }
            object obj = HttpContext.Current.Cache.Get(key);
            return obj == null ? default(T) : (T)obj;
        }
        public static T GetCache<T>(string key, Func<T> fnc, TimeSpan? MilliSeconds = null)
        {
            var cacheValue = Get<T>(key);
            if (cacheValue == null)
            {
                var lockob = LockDictionary.GetOrAdd(key, new object());
                lock (lockob)
                {
                    var newcache = Get<T>(key);
                    if (newcache != null)
                        return newcache;
                    cacheValue = fnc.Invoke();
                    Insert(key, cacheValue, MilliSeconds ??
                            (ConfigurationManager.AppSettings["LocalCacheExpiresMillSeconds"] == null ?
                            TimeSpan.FromMilliseconds(300000) : TimeSpan.FromMilliseconds(double.Parse(ConfigurationManager.AppSettings["LocalCacheExpiresMillSeconds"]))));
                }
            }
            return cacheValue;
        }

    }
}
