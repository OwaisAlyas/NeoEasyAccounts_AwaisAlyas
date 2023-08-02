using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace Neo.EasyAccounts.Web.UI.Caching
{
	public class InMemoryCache : ICacheService
	{
		public InMemoryCache()
		{

		}
		public T GetOrSet<T>(string cacheKey, int cacheTime, Func<T> getItemCallback) where T : class
		{
			T item = MemoryCache.Default.Get(cacheKey) as T;
			if (item == null)
			{
				item = getItemCallback();
				MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(cacheTime));
			}
			return item;
		}
	}

	public interface ICacheService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">Type of the item</typeparam>
		/// <param name="cacheKey">Name for the cached item</param>
		/// <param name="cacheTime">Time in Minutes</param>
		/// <param name="getItemCallback">Function to execute to get the item T</param>
		/// <returns></returns>
		T GetOrSet<T>(string cacheKey, int cacheTime, Func<T> getItemCallback) where T : class;
	}
}