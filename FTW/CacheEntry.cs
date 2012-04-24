using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.Threading;

namespace FTW.Web.Caching
{
	internal class CacheEntry
	{
		#region public object Locker

		private readonly object _Locker = new object();

		public object Locker
		{
			get { return this._Locker; }
		}

		#endregion

		#region public object InternalLocker

		private object _InternalLocker;

		public object InternalLocker
		{
			get
			{
				if (this._InternalLocker == null)
				{
					Interlocked.CompareExchange(ref this._InternalLocker, new object(), null);
				}
				return this._InternalLocker;
			}
		}

		#endregion

		#region public DateTime CreateDateTime

		private DateTime _CreateDateTime = DateTime.Now;

		public DateTime CreateDateTime
		{
			get { return this._CreateDateTime; }
		}

		#endregion

		#region public DateTime LastUse

		private DateTime _LastUse = DateTime.MinValue;

		public DateTime LastUse
		{
			get { return this._LastUse; }
			set { this._LastUse = value; }
		}

		#endregion

		#region public DateTime LastUpdate

		private DateTime _LastUpdate = DateTime.MaxValue;

		public DateTime LastUpdate
		{
			get { return this._LastUpdate; }
			set { this._LastUpdate = value; }
		}

		#endregion

		#region public TimeSpan Age

		public TimeSpan Age
		{
			get { return (this._LastUse - this._LastUpdate); }
		}

		#endregion

		#region public TimeSpan RefreshInterval

		private TimeSpan _RefreshInterval;

		public TimeSpan RefreshInterval
		{
			get { return this._RefreshInterval; }
			set { this._RefreshInterval = value; }
		}

		#endregion

		#region PersistToFile
		private bool _persistToFile = false;

		public bool PersistToFile
		{
			get { return _persistToFile; }
			set { _persistToFile = value; }
		}
		#endregion Persist To File

		#region PersistedFilename
		private string _persistedFilename;

		public string PersistedFilename
		{
			get { return _persistedFilename; }
			set { _persistedFilename = value; }
		}
		#endregion PersistedFilename

		#region public Timespan SlidingExpiration

		private TimeSpan _SlidingExpiration;

		public TimeSpan SlidingExpiration
		{
			get { return this._SlidingExpiration; }
			set { this._SlidingExpiration = value; }
		}

		#endregion

		#region public CacheItemPriority CacheItemPriority

		private CacheItemPriority _CacheItemPriority = CacheItemPriority.Normal;

		public CacheItemPriority CacheItemPriority
		{
			get { return this._CacheItemPriority; }
			set { this._CacheItemPriority = value; }
		}

		#endregion

		#region public TCache.CacheLoaderDelegate CacheLoader

		private TCache.CacheLoaderDelegate _CacheLoader;

		public TCache.CacheLoaderDelegate CacheLoader
		{
			get { return this._CacheLoader; }
			set { this._CacheLoader = value; }
		}

		#endregion

		#region Constructors

		internal CacheEntry()
		{ }

		#endregion
	}
}
