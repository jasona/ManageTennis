using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace BillRob.Web.Caching
{
	public abstract class CollectionThread
	{
		#region Fields
		protected int SlidingSeconds;
		protected int AbsoluteSeconds;
		protected bool IsDisposed = false;
		#endregion Fields


		#region Statics

		private static Thread ExecutionThread;
		internal static List<WeakReference> CollectionList;
		internal static object ListLock;

		static CollectionThread()
		{
			CollectionList = new List<WeakReference>();
			ListLock = new object();

			ExecutionThread = new Thread(Run);
			ExecutionThread.Name = "BillRob.Web.Caching.CollectionThread";
			ExecutionThread.IsBackground = true;
			ExecutionThread.Start();
		}

		private static void Run()
		{
			Thread.Sleep(2 * 60 * 1000);
			while (true)
			{
				//Debug.WriteLine("Running pruning iteration.");

				Queue<CollectionThread> q = new Queue<CollectionThread>(GetAndPurgeCollectionThreadsInCollection()); ;

				//process all of the queues.
				while (q.Count != 0)
				{
					q.Dequeue().ProcessQueue();
				}

				Thread.Sleep(5 * 1000);
			}
		}

		/// <summary>
		/// Will get all valid CollectionThread objects from the collection and remove the ones that have been released.
		/// </summary>
		/// <returns></returns>
		private static List<CollectionThread> GetAndPurgeCollectionThreadsInCollection()
		{
			List<CollectionThread> goodList = new List<CollectionThread>();
			//take a snapshot copy of the master list.
			lock (CollectionThread.ListLock)
			{
				for (int i = 0; i < CollectionList.Count; i++)
				{
					WeakReference reference = CollectionList[i];
					if (reference == null || reference.IsAlive == false || ((CollectionThread) reference.Target ) == null)
					{
						CollectionList.RemoveAt(i);
						i--;
					}
					else
					{
						goodList.Add(((CollectionThread)reference.Target));
					}
				}
			}

			return goodList;
		}

		/// <summary>
		/// Will 
		/// </summary>
		/// <returns></returns>
		public static List<CollectionCount> GetCollectionReport()
		{
			List<CollectionCount> list = new List<CollectionCount>();

			foreach (CollectionThread collection in GetAndPurgeCollectionThreadsInCollection())
			{
				list.Add(new CollectionCount(collection.Name, collection.GetSize()));
			}

			list.Sort((a, b) => a.Name.CompareTo(b.Name));

			return list;
		}
		#endregion Statics

		#region ctor
		/// <summary>
		/// Creates a new instance of the collection class.
		/// Will pull the ExpireSecondsAttribute and register the class with the static collection.
		/// </summary>
		public CollectionThread()
		{
			object[] array = this.GetType().GetCustomAttributes(typeof(ExpirationAttribute), true);

			if ( array.Length == 1 )
			{
				ExpirationAttribute attribute = array[0] as ExpirationAttribute;
				SlidingSeconds = attribute.SlidingSeconds;
				AbsoluteSeconds = attribute.AbsoluteSeconds;

				if (SlidingSeconds == 0 && AbsoluteSeconds == 0)
					AbsoluteSeconds = 60;
			}

			lock (CollectionThread.ListLock)
			{
				CollectionThread.CollectionList.Add(new WeakReference(this));
			}
		}
		#endregion ctor

		#region Abstract
		protected abstract void ProcessQueue();
		protected abstract int GetSize();
		/// <summary>
		/// Gets the name of the collection.
		/// </summary>
		public virtual string Name
		{
			get { return this.GetType().FullName; }
		}
		#endregion Abstract

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed) return;

			IsDisposed = true;

			List<CollectionThread> list = GetAndPurgeCollectionThreadsInCollection();

			if (list.Contains(this))
			{
				lock (CollectionThread.ListLock)
				{
					for (int i = 0; i < CollectionList.Count; i++)
					{
						if (CollectionList[i].Target == this)
						{
							CollectionList.RemoveAt(i);
							break;
						}
					}
				}
			}

			//nothing unmanaged so no need to use the dispoing parameter
		}

		/// <summary>
		/// will remove the class from the static collection for processing.
		/// </summary>
		~CollectionThread()
		{
			Dispose(false);
		}
		#endregion
	}

	public abstract class BaseItemCollection<TKey, TValue> : CollectionThread, IDisposable where TValue : class
	{
		private Queue<CollectionItemEntry<TKey, TValue>> SlidingQueue = new Queue<CollectionItemEntry<TKey, TValue>>();
		private Queue<CollectionItemEntry<TKey, TValue>> AbsoluteQueue = new Queue<CollectionItemEntry<TKey, TValue>>();
		private Dictionary<TKey, CollectionItemEntry<TKey, TValue>> Hash = new Dictionary<TKey, CollectionItemEntry<TKey, TValue>>();
		
		protected object CollectionLock = new object();
		
		#region Get

		/// <summary>
		/// Gets the item by the appropriate key.
		/// </summary>
		/// <param name="keyValue">The key of the item.</param>
		/// <returns>The item, or null if it isn't is the collection.</returns>
		public virtual TValue this[TKey keyValue]
		{
			get
			{
				return Get(keyValue);
			}
		}


		/// <summary>
		/// Gets the item by the appropriate key.
		/// </summary>
		/// <param name="keyValue">The key of the item.</param>
		/// <returns>The item, or null if it isn't is the collection.</returns>
		public virtual TValue Get(TKey keyValue)
		{
			return GetInternal(keyValue);
		}
		/// <summary>
		/// Gets the item by the appropriate key.
		/// </summary>
		/// <param name="keyValue">The key of the item.</param>
		/// <returns>The item, or null if it isn't is the collection.</returns>
		protected TValue GetInternal(TKey keyValue)
		{
			CollectionItemEntry<TKey, TValue> item = null;

			lock (CollectionLock)
			{
				if (false == Hash.TryGetValue(keyValue, out item))
					return null;
			}

			ItemRetreived(item);

			return item.Item;
		}

		#endregion Get

		#region Insert


		public virtual void Insert(TKey keyValue, TValue value)
		{
			lock (CollectionLock)
			{
				//if the item already exists just update its value.
				if (Hash.ContainsKey(keyValue))
				{
					CollectionItemEntry<TKey, TValue> entry = Hash[keyValue] as CollectionItemEntry<TKey, TValue>;
					entry.Item = value;
				}
				else
				{
					CollectionItemEntry<TKey, TValue> entry = new CollectionItemEntry<TKey, TValue>(keyValue, value);
					Hash[keyValue] = entry;
					if (AbsoluteSeconds != 0) AbsoluteQueue.Enqueue(entry);
					ItemRetreived(entry);
				}
			}
		}
		#endregion Insert

		#region Remove
		/// <summary>
		/// Will remove an item from the cache list.
		/// </summary>
		/// <param name="keyValue">The key to remove.</param>
		public virtual void Remove(TKey keyValue)
		{
			lock (CollectionLock)
			{
				if (Hash.ContainsKey(keyValue))
				{
					this.ItemRemoved(Hash[keyValue] as CollectionItemEntry<TKey, TValue>);
				}
			}
		}

		/// <summary>
		/// Method called after an item is removed from the collection.
		/// </summary>
		/// <param name="value">The item that was removed..</param>
		public virtual void OnItemRemoved(TValue value)
		{

		}

		#endregion Remove

		#region Refresh
		/// <summary>
		/// Will refresh an item in a background manner.  This currently calls remove.
		/// </summary>
		/// <param name="keyValue"></param>
		public virtual void Refresh(TKey keyValue)
		{
			Remove(keyValue);
		}
		#endregion Refresh

		#region GetSize
		/// <summary>
		/// Will get the size of the collection.
		/// </summary>
		/// <returns></returns>
		protected override int GetSize()
		{
			lock (CollectionLock)
			{
				return Hash.Count;
			}
		}
		#endregion GetSize

		#region ProcessQueue
		/// <summary>
		/// Will loop through the queue and remove any items from the hash table.
		/// </summary>
		protected override void ProcessQueue()
		{
			//do nothing if already removed and finalized.
			if (IsDisposed) return;

			Queue<CollectionItemEntry<TKey, TValue>> removalQueue = new Queue<CollectionItemEntry<TKey, TValue>>();

			ProcessSlidingQueue(removalQueue);
			ProcessAbsoluteQueue(removalQueue);

			lock (CollectionLock)
			{
				while (removalQueue.Count != 0)
				{
					ItemRemoved(removalQueue.Dequeue());
				}
			}
		}

		#region ProcessAbsoluteQueue
		private void ProcessAbsoluteQueue(Queue<CollectionItemEntry<TKey, TValue>> removalQueue)
		{
			while (AbsoluteSeconds > 0 && AbsoluteQueue.Count != 0)
			{
				CollectionItemEntry<TKey, TValue> item = AbsoluteQueue.Peek();

				if (item.IsTrash)
				{
					AbsoluteQueue.Dequeue();
					continue;
				}

				// no reference count check.

				// if the create time time span is older than now, remove the item.
				TimeSpan age = DateTime.Now - item.CreatedTime;
				if (age.TotalSeconds < AbsoluteSeconds)
				{
					break;
				}

				removalQueue.Enqueue(item);
				AbsoluteQueue.Dequeue();
			}
		}
		#endregion ProcessAbsoluteQueue

		#region ProcessSlidingQueue
		private void ProcessSlidingQueue(Queue<CollectionItemEntry<TKey, TValue>> removalQueue)
		{
			//walk expiration key
			while (SlidingSeconds > 0 && SlidingQueue.Count != 0)
			{
				CollectionItemEntry<TKey, TValue> item = SlidingQueue.Peek();

				//if it is trash, just popup it.
				if (item.IsTrash)
				{
					SlidingQueue.Dequeue();
					continue;
				}

				//if the item is in the queue more than once...skip this.
				if (item.ReferenceCount > 1)
				{
					item.ReferenceCount--;
					SlidingQueue.Dequeue();
					continue;
				}


				// if the age of the item is newer than the expire time, stop processing
				// anything behind this will be newer.
				TimeSpan age = DateTime.Now - item.LastUseTime;

				if (age.Seconds < SlidingSeconds)
				{
					break;
				}

				//remove the item because it is time.
				removalQueue.Enqueue(item);
				SlidingQueue.Dequeue();
			}
		}
		#endregion ProcessSlidingQueue

		#endregion ProcessQueue

		#region Private

		/// <summary>
		/// This method is called when an item is retreived.
		/// </summary>
		/// <param name="entry">The item that was retreived.</param>
		private void ItemRetreived(CollectionItemEntry<TKey, TValue> entry)
		{
			//doesn't do anything if not watching expire queue.
			if (SlidingSeconds <= 0) return;

			entry.LastUseTime = DateTime.Now;
			entry.ReferenceCount++;

			SlidingQueue.Enqueue(entry);
		}

		private void ItemRemoved(CollectionItemEntry<TKey, TValue> entry)
		{
			if (entry.IsTrash) return;

			Debug.WriteLine("Removed item: " + entry.Key + " from collection named: " + this.GetType().FullName );

			entry.IsTrash = true;
			if ( Hash.Remove(entry.Key) )
				OnItemRemoved(entry.Item);
		}
		#endregion Private

		#region Dispose
		protected override void Dispose(bool disposing)
		{
			if (IsDisposed) return;

			this.AbsoluteQueue.Clear();
			this.SlidingQueue.Clear();

			base.Dispose(disposing);
		}
		#endregion Dispose


	}
}
