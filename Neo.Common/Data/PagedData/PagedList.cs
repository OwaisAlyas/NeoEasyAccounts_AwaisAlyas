//namespace Neo.Common.Data.PagedData
//{
//	using System;
//	using System.Collections.Generic;
//	using System.ComponentModel;
//	using System.Linq;
//	using System.Linq.Expressions;
//	using System.Data.Entity;
//	using Neo.Common.Data.PagedData;

//	public class PagedList<T> : List<T>, IPagedList<T>
//	{
//		public PagedList(IDbSet<T> dbSet, int index, int pageSize, Expression<Func<T, bool>> filter, string[] includePaths, SortExpression<T>[] sortExpressions)
//			: this(dbSet.AsQueryable(), index, pageSize, filter, includePaths, sortExpressions)
//		{
//		}

//		public int PageCount { get; private set; }
//		public int TotalItemCount { get; private set; }
//		public int PageIndex { get; private set; }
//		public int PageNumber { get { return PageIndex + 1; } }
//		public int PageSize { get; private set; }
//		public bool HasPreviousPage { get; private set; }
//		public bool HasNextPage { get; private set; }
//		public bool IsFirstPage { get; private set; }
//		public bool IsLastPage { get; private set; }
//		public int ItemStart { get; private set; }
//		public int ItemEnd { get; private set; }

//		public PagedList(IDbSet<T> dbSet, int index, int pageSize, Expression<Func<T, bool>> filter, string[] includePaths, SortExpression<T>[] sortExpressions)
//		{
//			IQueryable<T> query = dbSet;
//			int totalRecords = query.Count();

//			if (filter != null) query = query.Where(filter);

//			if (includePaths != null && includePaths.Length > 0)
//			{
//				for (var i = 0; i < includePaths.Count(); i++)
//				{
//					query = query.Include(includePaths[i]);
//				}
//			}

//			if (sortExpressions != null)
//			{
//				IOrderedQueryable<T> orderedQuery = null;
//				for (var i = 0; i < sortExpressions.Count(); i++)
//				{
//					if (i == 0)
//					{
//						if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
//						{
//							orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
//						}
//						else
//						{
//							orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
//						}
//					}
//					else
//					{
//						if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
//						{
//							orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
//						}
//						else
//						{
//							orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
//						}
//					}
//				}
//				query = orderedQuery;
//			}

//			this.PageSize = pageSize;
//			this.PageIndex = index;
//			this.TotalItemCount = totalRecords;
//			this.PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;

//			this.HasPreviousPage = (PageIndex > 0);
//			this.HasNextPage = (PageIndex < (PageCount - 1));
//			this.IsFirstPage = (PageIndex <= 0);
//			this.IsLastPage = (PageIndex >= (PageCount - 1));

//			this.ItemStart = PageIndex * PageSize + 1;
//			this.ItemEnd = Math.Min(PageIndex * PageSize + PageSize, TotalItemCount);

//			if (TotalItemCount <= 0) return;

//			if (query != null && query.Count() > 0)
//			{
//				if (PageSize > 0)
//				{
//					AddRange(query.Take(PageSize));
//				}
//				if (PageIndex > 0)
//				{
//					AddRange(query.Skip(PageIndex * PageSize).Take(PageSize));
//				}
//			}

//			//if (realTotalCount != TotalItemCount)
//			//	AddRange(source.Take(PageSize));
//			//else
//			//	AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));

//		}

//		//public PagedList(IQueryable<T> source, int index, int pageSize, int totalCount)
//		//{
//		//	if (index < 0) throw new ArgumentOutOfRangeException("index", "Value can not be below 0.");
//		//	if (pageSize < 1) throw new ArgumentOutOfRangeException("pageSize", "Value can not be less than 1.");

//		//	if (source == null) source = new List<T>().AsQueryable();

//		//	var realTotalCount = source.Count();

//		//	PageSize = pageSize;
//		//	PageIndex = index;
//		//	TotalItemCount = totalCount > 0 ? totalCount : realTotalCount;
//		//	PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;

//		//	HasPreviousPage = (PageIndex > 0);
//		//	HasNextPage = (PageIndex < (PageCount - 1));
//		//	IsFirstPage = (PageIndex <= 0);
//		//	IsLastPage = (PageIndex >= (PageCount - 1));

//		//	ItemStart = PageIndex * PageSize + 1;
//		//	ItemEnd = Math.Min(PageIndex * PageSize + PageSize, TotalItemCount);

//		//	if (TotalItemCount <= 0) return;

//		//	if (realTotalCount != TotalItemCount)
//		//		AddRange(source.Take(PageSize));
//		//	else
//		//		AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
//		//}
//	}
//}