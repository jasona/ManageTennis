using System;
using System.Collections.Generic;
using System.Text;

namespace BillRob.Web.Caching
{
	public class CacheLoaderErrorException : Exception
	{
		#region Constructors

		public CacheLoaderErrorException()
			: base("The CacheLoaderDelegate should only be set once within an app.")
		{ }

		#endregion
	}
}
