using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BillRob.Web.Caching
{
	public abstract class MultipleItemCollection<TKey, TValue> : SingleItemCollection<TKey,TValue> where TValue : class
	{
		/// <summary>
		/// Method should be used to load up a bulk collection.  The key and the item should be returned as a sorted list.
		/// This is necessary because the list will be deconstructed back into the list before returning to 
		/// keep the order the same as was asked.
		/// </summary>
		/// <param name="list">The list of keys to look up.</param>
		/// <returns></returns>
		protected abstract SortedList<TKey, TValue> Load(TKey[] keys);

		/// <summary>
		/// Gets a list of items by key value.  Will reset the last used time.
		/// </summary>
		/// <param name="keys">The list of keys to retreive.</param>
		/// <returns></returns>
		protected List<TValue> Get(List<TKey> keys)
		{
			return Get(keys.ToArray());
		}

		/// <summary>
		/// Override for singular load. It can return null if the item doesn't exist.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected override TValue Load(TKey key)
		{
			TKey[] keys = new TKey[1];

			keys[0] = key;

			SortedList<TKey, TValue> list = Load(keys);
			if (list.Count == 1) return list.Values[0];
			if (list.Count == 0) return null;
			else throw new ArgumentException("Correct number of items not returned.  Should have been 1 or 0.  Missing key:" + key.ToString());
		}

		/// <summary>
		/// Gets a list of items by key value.  Will reset the last used time.
		/// </summary>
		/// <param name="list">The list of keys to retreive.</param>
		/// <returns></returns>
		public List<TValue> Get(TKey[] list)
		{
			SortedList<TKey, TValue> loadingList = new SortedList<TKey, TValue>();
			List<TKey> missingList = new List<TKey>();

			lock (CollectionLock)
			{
				//loop the asked for list and get it all from the cache
				//it will also build the master list to go load.
				foreach (TKey key in list)
				{
					TValue value = GetInternal(key);
					if (value == null && missingList.Contains(key) == false) missingList.Add(key);
					else if (value != null && loadingList.ContainsKey(key) == false) loadingList.Add(key, value);
				}
			}

			if (missingList.Count != 0)
			{
				//load from the abstract method and put it into the list before pulling back.
				foreach (KeyValuePair<TKey, TValue> pair in Load(missingList.ToArray()))
				{
					Insert(pair.Key, pair.Value);
					if ( loadingList.ContainsKey( pair.Key ) == false ) loadingList.Add(pair.Key, pair.Value);
				}
			}

			//build the final list.
			List<TValue> returningList = new List<TValue>();
			foreach (TKey key in list)
			{
				if (loadingList.ContainsKey(key) == false)
					throw new ArgumentNullException("Could not find key: " + key + " in the collection.");

				returningList.Add(loadingList[key]);
			}
			return returningList;
		}

		/// <summary>
		/// Will remove a list of items
		/// </summary>
		/// <param name="keys">The keys to remove.</param>
		public void Remove(TKey[] keys )
		{
			lock (CollectionLock)
			{
				foreach (TKey key in keys)
					Remove(key);
			}
		}
	}
}
