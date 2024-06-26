﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Neo.EasyAccounts
{
	public static class PagingExtensions
	{
		public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
		{
			return new Pager(htmlHelper, pageSize, currentPage, totalItemCount);
		}
		public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
		{
			return new Pager(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
		}
		public static Pager<TModel> Pager<TModel>(this HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount)
		{
			return new Pager<TModel>(htmlHelper, pageSize, currentPage, totalItemCount);
		}
		public static Pager<TModel> Pager<TModel>(this HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
		{
			return new Pager<TModel>(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
		}

		//public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int? totalCount = null)
		//{
		//	return new PagedList<T>(source, pageIndex, pageSize, totalCount);
		//}

		//public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int? totalCount = null)
		//{
		//	return new PagedList<T>(source, pageIndex, pageSize, totalCount);
		//}
	}
}