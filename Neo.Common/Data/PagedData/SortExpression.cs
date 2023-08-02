namespace Neo.Common.Data.PagedData
{
	using System;
	using System.ComponentModel;
	using System.Linq.Expressions;

	public class SortExpression<T>
	{
		public SortExpression(Expression<Func<T, object>> sortBy, ListSortDirection sortDirection)
		{
			SortBy = sortBy;
			SortDirection = sortDirection;
		}

		public Expression<Func<T, object>> SortBy { get; set; }
		public ListSortDirection SortDirection { get; set; }
	}
}
