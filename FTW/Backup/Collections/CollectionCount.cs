using System;
using System.Collections.Generic;
using System.Text;

namespace BillRob.Web.Caching
{
	/// <summary>
	/// Class for holding a name / size pair.
	/// </summary>
	public class CollectionCount
	{
		public CollectionCount(string name, int size)
		{
			Name = name;
			Size = size;
		}

		/// <summary>
		/// Gets the name of the collection.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the Size of the collection.
		/// </summary>
		public int Size
		{
			get;
			private set;
		}

	}
}
