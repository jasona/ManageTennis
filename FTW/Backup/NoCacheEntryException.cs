using System;
using System.Collections.Generic;
using System.Text;

namespace BillRob.Web.Caching
{
	public class NoCacheEntryException : Exception
	{
		// could come from a resource file
		private const string EXCEPTION_FORMAT = "CacheEntry for key '{0}' doesn't exist.  Please call a different overload of TCache<T>.Get() to set the CacheEntry properties.";

		#region Constructors

		public NoCacheEntryException(string cacheKey)
			: base(GetMessage(cacheKey))
		{ }

		#endregion

		#region private static string GetMessage(string cacheKey)

		private static string GetMessage(string cacheKey)
		{
			return string.Format(EXCEPTION_FORMAT, cacheKey);
		}

		#endregion
	}
}
	