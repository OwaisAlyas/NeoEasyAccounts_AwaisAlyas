using Neo.Common.Data.PagedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts
{
	public static class PagingUtil
	{
		//public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, Expression<Func<T, bool>> filter, string[] includePaths, SortExpression<T>[] sortExpressions)
		//{
		//	return new PagedList<T>(source, pageIndex, pageSize, filter, includePaths, sortExpressions);
		//}

		//public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, Expression<Func<T, bool>> filter, string[] includePaths, SortExpression<T>[] sortExpressions)
		//{
		//	return new PagedList<T>(source, pageIndex, pageSize, filter, includePaths, sortExpressions);
		//}

		public static IPagedData<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedData<T>(pageIndex, pageSize, totalCount, source.ToList());
		}
		public static IPagedData<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedData<T>(pageIndex, pageSize, totalCount, source.ToList());
		}
		public static IPagedData<T> ToPagedList<T>(this List<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedData<T>(pageIndex, pageSize, totalCount, source);
		}
	}
}