using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace BillRob.Web.Caching
{

	public static class TCache
	{
		#region Static variables

		private static readonly Cache _cache;

		private static CacheLoaderErrorDelegate _cacheLoaderErrorDelegate = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Static initializer should ensure we only have to look up the current cache
		/// instance once.
		/// </summary>
		static TCache()
		{
			// HttpContent.Current.Cache always returns HttpRuntime.Cache
			//HttpContext context = HttpContext.Current;
			_cache = HttpRuntime.Cache;
			Timer t = new Timer(PersistToFileCleanup, null, 4 * 1000, 1 * 60 * 60 * 1000);
		}
		#endregion

		#region PersistToFileCleanup
		private static void PersistToFileCleanup(object o)
		{
			try
			{
				string basePath = null;
				System.Web.HttpContext context = System.Web.HttpContext.Current;
				if (context == null)
					basePath = AppDomain.CurrentDomain.BaseDirectory;
				else
					basePath = context.Server.MapPath("~/");

				basePath = Path.Combine(basePath, "LocalCache");

				if (Directory.Exists(basePath) == false) Directory.CreateDirectory(basePath);

				DateTime badDate = DateTime.Now.AddDays(-1);
				foreach (FileInfo file in new DirectoryInfo(basePath).GetFiles())
				{
					if (file.LastWriteTime < badDate)
					{
						try
						{
							file.Delete();
						}
						catch { }
					}
				}
			}
			catch { }

		}
		#endregion PersistToFileCleanup

		#region public delegates

		public delegate object CacheLoaderDelegate();

		public delegate void CacheLoaderErrorDelegate(string cacheKey, Exception e);

		#endregion

		#region private static IDictionaryEnumerator GetCacheEnumerator()

		private static IDictionaryEnumerator GetCacheEnumerator()
		{
			return _cache.GetEnumerator();
		}

		#endregion

		#region public static void Clear()

		/// <summary>
		/// Removes all items from the Cache
		/// </summary>
		public static void Clear()
		{
			IDictionaryEnumerator CacheEnum = GetCacheEnumerator();
			ArrayList al = new ArrayList();
			while (CacheEnum.MoveNext())
			{
				al.Add(CacheEnum.Key);
			}

			foreach (string key in al)
			{
				Remove(key);
			}

		}

		#endregion

		#region public static void RemoveByPattern(string pattern)

		public static void RemoveByPattern(string pattern)
		{
			IDictionaryEnumerator CacheEnum = GetCacheEnumerator();
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
			while (CacheEnum.MoveNext())
			{
				string key = CacheEnum.Key.ToString();
				if (regex.IsMatch(key))
					Remove(key);
			}
		}

		#endregion

		#region public static void Remove(string key)

		/// <summary>
		/// Removes the specified key from the cache
		/// </summary>
		/// <param name="key"></param>
		public static void Remove(string key)
		{
			_cache.Remove(key);
		}

		#endregion

		#region private static void BaseInsert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="dependencies"></param>
		/// <param name="absoluteExpiration"></param>
		/// <param name="slidingExpiration"></param>
		/// <param name="priority"></param>
		/// <param name="onRemoveCallback"></param>
		private static void BaseInsert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
		{
			if (value != null)
			{
				_cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
			}
		}

		#endregion

		#region public static object Get(string key)

		public static object Get(string key)
		{
			return _cache[key];
		}

		#endregion

		#region internal static object InternalCallback(string key)

		internal static object InternalCallback(string key)
		{
			return InternalCallback(key, false);
		}

		internal static object InternalCallback(string key, bool allowThrow)
		{
			CacheEntry entry = CacheLockbox.GetCacheEntry(key);

			if (entry == null) return null;

			CacheLoaderDelegate cacheLoader = entry.CacheLoader;

			if (cacheLoader == null) return null;

			object o = null;
			try
			{
				o = cacheLoader();
			}
			catch (Exception e)
			{
				if (allowThrow)
					throw;

				CacheLoaderErrorDelegate handler = _cacheLoaderErrorDelegate;

				if (handler != null)
				{
					try
					{
						handler(key, e);
					}
					catch { } /* prevent the external code from causing this thread to abort abruptly */
				}
			}

			if (o != null)
			{
				DateTime now = DateTime.Now;
				TimeSpan computedTimeSpan = DateTime.MaxValue - now;
				DateTime expirationDateTime = (TimeSpan.Compare(computedTimeSpan, entry.RefreshInterval) == 1)
					? now.Add(entry.RefreshInterval)
					: DateTime.MaxValue;

				BaseInsert(
					key
					, o
					, null
					, expirationDateTime
					, Cache.NoSlidingExpiration
					, entry.CacheItemPriority
					, new CacheItemRemovedCallback(ItemRemovedCallback));


				if (entry.PersistToFile)
				{
					try
					{
						DataContractSerializer formatter = new DataContractSerializer(o.GetType());
						//BinaryFormatter formatter = new BinaryFormatter();
						using (FileStream fileStream = new FileStream(entry.PersistedFilename, FileMode.OpenOrCreate, FileAccess.Write))
						{
							formatter.WriteObject(fileStream, o);
						}
					}
					catch
					{
						if (allowThrow)
							throw;
					}
				}



				CacheLockbox.UpdateCacheEntry(key, now);
			}

			return o;
		}

		#endregion

		#region internal static void ItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)

		internal static void ItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.Expired)
			{
				CacheEntry entry = CacheLockbox.GetCacheEntry(key);

				// this means no one accessed this since the last population perhaps this should be checking for a specific span,
				// but > 0 seem to be good enough this is where we would control if the data gets back in cache or not. For instance,
				// the 'else' condition here could put the data back in the cache without the callback and just let it expire naturally
				if (entry.LastUse.Add(entry.SlidingExpiration) > DateTime.Now)
				{
					// not sure if TCache should be retrofit to just wrap this full call
					BaseInsert(
						key
						, value
						, null
						// not sure this should be configurable, but 30 seconds is the default timeout for ADO.NET
						// we will take the minimum timeout for this cache entry
						, DateTime.Now.AddSeconds(Math.Min(entry.RefreshInterval.TotalSeconds, 30L))
						, Cache.NoSlidingExpiration
						// again, not sure if this should be configurable or not - 'Low' should give ASP.NET the breathing room it needs
						, CacheItemPriority.Low
						, null);

					EnqueueCallbackMethod(key);
				}
			}
		}

		internal static void EnqueueCallbackMethod(string key)
		{
			ManagedThreadPool.QueueUserWorkItem(delegate(object o)
			{
				lock (CacheLockbox.GetInternalLock(key))
				{
					InternalCallback(key);
				}
			});
		}

		#endregion

		#region public static void RefreshByPattern(string pattern)

		public static void RefreshByPattern(string pattern)
		{
			ThreadPool.QueueUserWorkItem(
				delegate(object _pattern)
				{
					IEnumerator<string> enumerator = CacheLockbox.Keys.GetEnumerator();

					Regex regex = new Regex(
						_pattern.ToString()
						, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase
						);

					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;

						if (regex.IsMatch(key))
						{
							// should the matched entries be handled as such
							// or should each be placed into the ManagedThreadPool?
							// I believe each method to be reasonable, but could the MTP
							// be "overloaded" with tasks?
							lock (CacheLockbox.GetInternalLock(key))
							{
								InternalCallback(key);
							}
						}
					}
				}
                , pattern
				);
		}

		#endregion

		#region public static bool RefreshByCacheKey(string key)

		public static bool RefreshByCacheKey(string key)
		{
			if (CacheLockbox.ContainsCacheEntry(key))
			{
				ThreadPool.QueueUserWorkItem(
					delegate(object o)
					{
						string _key = o.ToString();

						Update(_key);
					}
                    , key
					);

				return true;
			}

			return false;
		}

		#endregion

		#region public static bool Update(string key)

		public static bool Update(string key)
		{
			if (CacheLockbox.ContainsCacheEntry(key))
			{
				lock (CacheLockbox.GetInternalLock(key))
				{
					InternalCallback(key, true);
				}

				return true;
			}

			return false;
		}

		#endregion

		#region public static void SetCacheLoaderErrorHandler(CacheLoaderDelegate handler)

		public static void SetCacheLoaderErrorHandler(CacheLoaderErrorDelegate handler)
		{
			if (_cacheLoaderErrorDelegate != null)
				throw new CacheLoaderErrorException();

			if (handler != null)
				_cacheLoaderErrorDelegate = handler;
		}

		#endregion

		#region public static object GetCacheEntryLock(...) overloads

		public static object GetCacheEntryLock(string key)
		{
			return CacheLockbox.GetLock(key);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="refreshIntervalSeconds">This is not an absolute time span - the TCache.*Factor statics variables affect this value only on the initial call to this method</param>
		/// <param name="slidingExpirationSeconds">This is not an absolute time span - the TCache.*Factor statics variables affect this value only on the initial call to this method</param>
		/// <param name="loaderDelegate"></param>
		/// <returns></returns>
		public static object GetCacheEntryLock(string key, int refreshIntervalSeconds, int slidingExpirationSeconds, CacheLoaderDelegate loaderDelegate)
		{
			return GetCacheEntryLock(key, refreshIntervalSeconds, slidingExpirationSeconds, false, CacheItemPriority.Normal, loaderDelegate);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="refreshInterval">This is an absolute time span - the TCache.*Factor statics variables do not affect this value</param>
		/// <param name="slidingExpiration">This is an absolute time span - the TCache.*Factor statics variables do not affect this value</param>
		/// <param name="loaderDelegate"></param>
		/// <returns></returns>
		public static object GetCacheEntryLock(string key, TimeSpan refreshInterval, TimeSpan slidingExpiration, CacheLoaderDelegate loaderDelegate)
		{
			return GetCacheEntryLock(key, refreshInterval, slidingExpiration, CacheItemPriority.Normal, loaderDelegate);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="refreshIntervalSeconds">This is not an absolute time span - the TCache.*Factor statics variables affect this value only on the initial call to this method</param>
		/// <param name="slidingExpirationSeconds">This is not an absolute time span - the TCache.*Factor statics variables affect this value only on the initial call to this method</param>
		/// <param name="persistToFile">Determines whether the results will be persisted to a file.</param>
		/// <param name="priority"></param>
		/// <param name="loaderDelegate"></param>
		/// <returns></returns>
		public static object GetCacheEntryLock(string key, int refreshIntervalSeconds, int slidingExpirationSeconds, bool persistToFile, CacheItemPriority priority, CacheLoaderDelegate loaderDelegate)
		{
			return CacheLockbox.GetLock(
				key
				, TimeSpan.FromSeconds((double)refreshIntervalSeconds)
				, TimeSpan.FromSeconds((double)slidingExpirationSeconds)
				, persistToFile
				, priority
				, loaderDelegate
				);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="refreshInterval">This is an absolute time span - the TCache.*Factor statics variables do not affect this value</param>
		/// <param name="slidingExpiration">This is an absolute time span - the TCache.*Factor statics variables do not affect this value</param>
		/// <param name="priority"></param>
		/// <param name="loaderDelegate"></param>
		/// <returns></returns>
		public static object GetCacheEntryLock(string key, TimeSpan refreshInterval, TimeSpan slidingExpiration, CacheItemPriority priority, CacheLoaderDelegate loaderDelegate)
		{
			return CacheLockbox.GetLock(key, refreshInterval, slidingExpiration, false, priority, loaderDelegate);
		}

		#endregion

		#region public static bool ContainsCacheEntry(string key)

		public static bool ContainsCacheEntry(string key)
		{
			return CacheLockbox.ContainsCacheEntry(key);
		}

		#endregion
	}

	public static class TCache<T> where T : class
	{
		#region public static T Get(...) overloads

		public static T Get1(string key)
		{
			if (CacheLockbox.ContainsCacheEntry(key))
			{
				lock (CacheLockbox.GetLock(key))
				{
					T t = TCache.Get(key) as T;

					if (t == null)
					{
						t = TCache.InternalCallback(key) as T;
					}

					return t;
				}
			}

			throw new NoCacheEntryException(key);
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="refreshInterval">This is an absolute time span - the TCache.*Factor statics variables do not affect this value</param>
		/// <param name="slidingExpiration">This is an absolute time span - the TCache.*Factor statics variables do not affect this value</param>
		/// <param name="persistToFile">Determines whether the object is persisted to a file.</param>
		/// <param name="priority">The caching priority to use.</param>
		/// <param name="loaderDelegate">The delegate loader.</param>
		/// <returns></returns>
		public static T Get(string key, TimeSpan refreshInterval, TimeSpan slidingExpiration, bool persistToFile, CacheItemPriority priority, TCache.CacheLoaderDelegate loaderDelegate)
		{
			lock (CacheLockbox.GetLock(key, refreshInterval, slidingExpiration, persistToFile, priority, loaderDelegate))
			{
				T t = TCache.Get(key) as T;

				if (t == null)
				{
					if (persistToFile == false)
					{
						return TCache.InternalCallback(key, true) as T;
					}
					else
					{
						CacheEntry entry = CacheLockbox.GetCacheEntry(key);
						string filename = entry.PersistedFilename;
						if ( File.Exists( filename ) == false )
							return TCache.InternalCallback(key, true) as T;

						try
						{
							DataContractSerializer formatter = new DataContractSerializer(typeof(T));
							using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
							{
								t = formatter.ReadObject(fileStream) as T;
								if (t != null)
								{
									TCache.EnqueueCallbackMethod(key); //set the callback to refresh in the background.
									return t;
								}
								
								return TCache.InternalCallback(key, true) as T;
							}
							
						}
						catch
						{
							return TCache.InternalCallback(key, true) as T;
						}
					}
				}

				return t;
			}
		}

		#endregion

		#region public static bool Update(string key)

		public static bool Update(string key)
		{
			return TCache.Update(key);
		}

		#endregion
	}
}
