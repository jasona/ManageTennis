using System;
using System.Collections.Generic;
using System.Text;

namespace FTW.Web.Caching
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
