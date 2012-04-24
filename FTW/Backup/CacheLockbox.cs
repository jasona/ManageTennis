using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.IO;

namespace BillRob.Web.Caching
{
	internal static class CacheLockbox
	{
		#region Private Static Fields

		private static readonly object _ReadLockbox = new object();

		private static readonly object _WriteLockbox = new object();

		private static readonly Dictionary<string, CacheEntry> _Lockbox = new Dictionary<string, CacheEntry>();

		#endregion

		#region private static Dictionary<string,CacheEntry> Instance

		private static Dictionary<string, CacheEntry> Instance
		{
			get { return _Lockbox; }
		}

		#endregion

		#region public static Dictionary<string,CacheEntry>.KeyCollection Keys

		public static Dictionary<string, CacheEntry>.KeyCollection Keys
		{
			get { return Instance.Keys; }
		}

		#endregion

		#region Constructors

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static CacheLockbox()
		{ }

		#endregion

		#region public static bool ContainsCacheEntry(string key)

		public static bool ContainsCacheEntry(string key)
		{
			return Instance.ContainsKey(key);
		}

		#endregion

		#region public static object GetLock(...) overloads

		public static object GetLock(string key)
		{
			lock (_ReadLockbox) // could use Instance.SyncRoot for all access
			{
				CacheEntry entry = null;

				if (ContainsCacheEntry(key))
				{
					entry = Instance[key];
				}

				if (entry != null)
				{
					entry.LastUse = DateTime.Now;

					return entry.Locker;
				}

				return new NoCacheEntryException(key);
			}
		}

		public static object GetLock(string key, TimeSpan refreshInterval, TimeSpan slidingExpiration, bool persistToFile, CacheItemPriority priority, TCache.CacheLoaderDelegate cacheLoader)
		{
			lock (_ReadLockbox) // could use Instance.SyncRoot for all access
			{
				CacheEntry entry;

				if (ContainsCacheEntry(key))
				{
					entry = Instance[key];
					entry.LastUse = DateTime.Now;
				}
				else
				{
					// escalate to a writer lock for this operation
					lock (_WriteLockbox) // could use Instance.SyncRoot for all access
					{
						entry = new CacheEntry();
						entry.CacheLoader = cacheLoader;
						entry.RefreshInterval = refreshInterval;
						entry.SlidingExpiration = slidingExpiration;
						entry.CacheItemPriority = priority;
						entry.LastUse = DateTime.Now;
						entry.PersistToFile = persistToFile;

						if (persistToFile)
						{
							string filename = key;
							foreach (char c in Path.GetInvalidFileNameChars())
							{
								filename = filename.Replace( c.ToString(), "^_^" );
							}
							filename += ".bin";
							if (filename.Length > 255) throw new ApplicationException("Can't use persist to key name is greater than 255.");

							string basePath = null;
							System.Web.HttpContext context = System.Web.HttpContext.Current;
							if (context == null)
								basePath = AppDomain.CurrentDomain.BaseDirectory;
							else
								basePath = context.Server.MapPath("~/");

							basePath = Path.Combine(basePath, "LocalCache");

							if (Directory.Exists(basePath) == false) Directory.CreateDirectory(basePath);

							entry.PersistedFilename = Path.Combine(basePath, filename);
						}

						Instance.Add(key, entry);
					}
				}

				return entry.Locker;
			}
		}

		#endregion

		#region public static object GetInternalLock(string key)

		public static object GetInternalLock(string key)
		{
			lock (_ReadLockbox) // could use Instance.SyncRoot for all access
			{
				CacheEntry entry = null;

				if (ContainsCacheEntry(key))
				{
					entry = Instance[key];
				}

				if (entry != null)
				{
					return entry.InternalLocker;
				}

				return null;
			}
		}

		#endregion

		#region public static void UpdateCacheEntry(...) overloads

		// not sure if this should be a delegate or not.
		// Depends on how much control should be given to
		public static void UpdateCacheEntry(string key, DateTime lastUpdate)
		{
			lock (_WriteLockbox) // could use Instance.SyncRoot for all access
			{
				if (ContainsCacheEntry(key))
				{
					CacheEntry entry = Instance[key];
					entry.LastUpdate = lastUpdate;
				}
			}
		}

		#endregion

		#region public static CacheEntry GetCacheEntry(string key)

		public static CacheEntry GetCacheEntry(string key)
		{
			lock (_ReadLockbox) // could use Instance.SyncRoot for all access
			{
				if (ContainsCacheEntry(key))
				{
					return Instance[key];
				}

				return null;
			}
		}

		#endregion
	}
}
