using System;
using System.Collections.Generic;
using System.Text;

namespace FTW.Web.Caching
{
	/// <summary>
	/// Attribute used on a collection class that will set the time expiration.
	/// </summary>
	[AttributeUsage( AttributeTargets.Class )]
	public class ExpirationAttribute : Attribute
	{
		private int _absoluteSeconds = 0;
		private int _slidingSeconds = 0;

		/// <summary>
		/// Gets the amount of time of inactivity to expire an item.
		/// </summary>
		public int SlidingSeconds
		{
			get { return _slidingSeconds; }
			set { _slidingSeconds = value; }
		}


		/// <summary>
		/// Gets or sets the absolute expiration time.
		/// </summary>
		public int AbsoluteSeconds
		{
			get { return _absoluteSeconds; }
			set { _absoluteSeconds = value; }
		}
	
	
	
	}
}
