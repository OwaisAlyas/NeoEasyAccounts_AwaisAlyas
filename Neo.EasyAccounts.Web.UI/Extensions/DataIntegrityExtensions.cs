using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts
{
	public static class DataIntegrityExtensions
	{
		public static bool HasValue(this string val)
		{
			bool check = (val != null && val.Length > 0);
			return check;
		}
		public static bool IsOdd(this int value)
		{
			return value % 2 != 0;
		}
		public static bool IsEven(this int value)
		{
			return value % 2 == 0;
		}

		public static bool Between(this IComparable a, IComparable b, IComparable c)
		{
			return a.CompareTo(b) >= 0 && a.CompareTo(c) <= 0;
		}

		public static bool HasValue<T>(this IEnumerable<T> val)
		{
			bool check = (val != null && val.Count() > 0);
			return check;
		}
	}
}