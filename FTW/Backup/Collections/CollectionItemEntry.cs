using System;
using System.Collections.Generic;
using System.Text;

namespace BillRob.Web.Caching
{
	internal class CollectionItemEntry<TKey, TValue>
	{
		private TValue _item;
		private DateTime _lastUseTime = DateTime.Now;
		private DateTime _createdTime = DateTime.Now;
		private DateTime _lastUpdateTime = DateTime.Now;

		private bool _isTrash = false;
		private int _referenceCount;
		private TKey _key;

		/// <summary>
		/// Creates a new entry item.
		/// </summary>
		/// <param name="key">The key the item is associated with.</param>
		/// <param name="value">The value of the item.</param>
		public CollectionItemEntry(TKey key, TValue value)
		{
			_key = key;
			Item = value;
		}

		/// <summary>
		/// Gets the time the item was last updated.
		/// </summary>
		public DateTime LastUpdateTime
		{
			get { return _lastUpdateTime; }
		}

		/// <summary>
		/// Gets or sets when the item was created.
		/// </summary>
		public DateTime CreatedTime
		{
			get { return _createdTime; }
			set { _createdTime = value; }
		}

		/// <summary>
		/// Gets or sets the key value for the item.
		/// </summary>
		public TKey Key
		{
			get { return _key; }
			set { _key = value; }
		}

		/// <summary>
		/// Gets the Item of data itself.
		/// </summary>
		public TValue Item
		{
			get { return _item; }
			set { _item = value; }
		}

		/// <summary>
		/// Gets the number of times this item is in the remove queue.
		/// </summary>
		public int ReferenceCount
		{
			get { return _referenceCount; }
			set { _referenceCount = value; }
		}
	

		/// <summary>
		/// Gets whether this item has been removed from the collection.
		/// </summary>
		public bool IsTrash
		{
			get { return _isTrash; }
			set { _isTrash = value; }
		}
	
		/// <summary>
		/// Gets the last time this object was used.
		/// </summary>
		public DateTime LastUseTime
		{
			get { return _lastUseTime; }
			set { _lastUseTime = value; }
		}
	}
}
