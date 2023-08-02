using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Common.Data.PagedData
{
	public interface IPagedData<T>
	{
		int PageCount { get; }
		int TotalItemCount { get; }
		int PageIndex { get; }
		int PageNumber { get; }
		int PageSize { get; }
		bool HasPreviousPage { get; }
		bool HasNextPage { get; }
		bool IsFirstPage { get; }
		bool IsLastPage { get; }
		int ItemStart { get; }
		int ItemEnd { get; }
		IList<T> Data { get; }
	}
	public class PagedData<T> : IPagedData<T>
	{
		public PagedData(int index, int pageSize, int totalCount, IList<T> data)
		{
			this.PageSize = pageSize;
			this.PageIndex = index;
			this.TotalItemCount = totalCount;
			this.PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;

			this.HasPreviousPage = (PageIndex > 0);
			this.HasNextPage = (PageIndex < (PageCount - 1));
			this.IsFirstPage = (PageIndex <= 0);
			this.IsLastPage = (PageIndex >= (PageCount - 1));

			this.ItemStart = PageIndex * PageSize + 1;
			this.ItemEnd = Math.Min(PageIndex * PageSize + PageSize, TotalItemCount);
			this.Data = data;
		}

		public IList<T> Data { get; private set; }

		public int PageCount { get; private set; }
		public int TotalItemCount { get; private set; }
		public int PageIndex { get; private set; }
		public int PageNumber { get { return PageIndex + 1; } }
		public int PageSize { get; private set; }
		public bool HasPreviousPage { get; private set; }
		public bool HasNextPage { get; private set; }
		public bool IsFirstPage { get; private set; }
		public bool IsLastPage { get; private set; }
		public int ItemStart { get; private set; }
		public int ItemEnd { get; private set; }
	}
}
