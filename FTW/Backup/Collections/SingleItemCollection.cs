using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace BillRob.Web.Caching
{
	public abstract class SingleItemCollection<TKey, TValue> : BaseItemCollection<TKey, TValue> where TValue : class
	{
		/// <summary>
		/// Will get the specified entry from the collection.  If the item doesn't exist in the collection it will be 
		/// loaded from cache.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public override TValue Get(TKey key)
		{
			TValue value = base.Get(key);

			if (value == null)
			{
				value = Load(key);
				Insert(key, value);
			}
			return value;
		}


		/// <summary>
		/// This method must be overloaded and will be called by a Get call when the item doesn't exist in the cache collection.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected abstract TValue Load(TKey key);
		
	}
}
