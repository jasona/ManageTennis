using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using FTW.Web.Caching;
using Oasis.Lib.Components;

namespace DMag.Components
{
    /// <summary>
    /// Summary description for DCache.
    /// </summary>
    public static class OCache
    {
        /// <summary>
        /// Static initializer should ensure we only have to look up the current cache
        /// instance once.
        /// </summary>
        static OCache()
        {
        }

        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void Clear()
        {
            TCache.Clear();
        }

        public static void RemoveByPattern(string pattern)
        {
            TCache.RemoveByPattern(pattern);
        }

        /// <summary>
        /// Removes the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            TCache.Remove(key);
        }

        public static object Get(string key)
        {
            return TCache.Get(key);
        }

        public static bool RefreshByCacheKey(string key)
        {
            return TCache.RefreshByCacheKey(key);
        }

        public static void RefreshByPattern(string pattern)
        {
            TCache.RefreshByPattern(pattern);
        }


        /// <summary>
        /// Calling this method will block the current thread until the object is refreshed in cache.  This should be 
        /// used when the cacheable is set to false before calling the .Get
        /// </summary>
        /// <param name="key">The cache key to refresh.</param>
        /// <returns></returns>
        public static bool Update(string key)
        {
            return TCache.Update(key);
        }

        public static bool ContainsCacheEntry(string key)
        {
            return TCache.ContainsCacheEntry(key);
        }
    }

    public static class OCache<T> where T : class
    {
        public static T Get(string key, TCache.CacheLoaderDelegate loaderDelegate)
        {
            return Get(key, CacheOptions.Normal, loaderDelegate);
        }

        public static T Get(string key, CacheOptions duration, TCache.CacheLoaderDelegate loaderDelegate)
        {
            int refresh = 60;
            int sliding = 60;
            bool persistToFile = false;

            //check the duration time.
            if (CacheOptions.Permanent == (duration & CacheOptions.Permanent))
            {
                sliding = Int32.MaxValue;
                refresh = Int32.MaxValue;
            }
            else if (CacheOptions.Long == (duration & CacheOptions.Long))
            {
                sliding = 10 * 60;
                refresh = 5 * 60;
            }
            else if (CacheOptions.Short == (duration & CacheOptions.Short))
            {
                sliding = 2 * 60;
                refresh = 1 * 60;
            }
            else if (CacheOptions.Normal == (duration & CacheOptions.Normal))
            {
                sliding = 5 * 60;
                refresh = 2 * 60;
            }
            else if (CacheOptions.VeryShort == (duration & CacheOptions.VeryShort))
            {
                sliding = 5 * 60;
                refresh = 10;
            }

            //check the interval time.
            if (CacheOptions.NoRefresh == (duration & CacheOptions.NoRefresh)) //normal
            {
                refresh = Int32.MaxValue;
            }
            else if (CacheOptions.ShortRefresh == (duration & CacheOptions.ShortRefresh))
            {
                refresh = 1 * 60;
            }
            else if (CacheOptions.MediumRefresh == (duration & CacheOptions.MediumRefresh))
            {
                refresh = 2 * 60;
            }
            else if (CacheOptions.LongRefresh == (duration & CacheOptions.LongRefresh))
            {
                refresh = 5 * 60;
            }
            else if (CacheOptions.LongestRefresh == (duration & CacheOptions.LongestRefresh))
            {
                refresh = 10 * 60;
            }
            else if (CacheOptions.ImmediateRefresh == (duration & CacheOptions.ImmediateRefresh))
            {
                refresh = 10;
            }

            if (CacheOptions.PersistToFile == (duration & CacheOptions.PersistToFile))
            {
                persistToFile = true;
            }


            return TCache<T>.Get(
                key
                , TimeSpan.FromSeconds(refresh)
                , TimeSpan.FromSeconds(sliding)
                , persistToFile
                , CacheItemPriority.Normal
                , loaderDelegate);
        }

        public static bool Update(string key)
        {
            return TCache<T>.Update(key);
        }
    }
}