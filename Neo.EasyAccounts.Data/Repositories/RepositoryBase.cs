using Neo.Common.Data.PagedData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories
{
	public abstract class RepositoryBase<T> : IRepository<T> where T : class
	{
		private readonly IDbSet<T> dbSet;
		protected IDbFactory DbFactory { get; private set; }

		private DbEntities _dbContext;
		protected DbEntities DbContext
		{
			get { return _dbContext ?? (_dbContext = DbFactory.Init()); }
		}

		public RepositoryBase(IDbFactory dbFactory)
		{
			DbFactory = dbFactory;
			dbSet = DbContext.Set<T>();
		}

		public virtual T Insert(T model)
		{
			dbSet.Add(model);
			return model;
		}
		public virtual T Update(T model)
		{
			dbSet.Attach(model);

			DbContext.Entry<T>(model).State = EntityState.Modified;

			return model;
		}
		public virtual T Delete(T model)
		{
			return dbSet.Remove(model);
		}
		public virtual void Reload(T model)
		{
			DbContext.Entry<T>(model).Reload();
		}
		public virtual async void ReloadAsync(T model)
		{
			await DbContext.Entry<T>(model).ReloadAsync();
		}

		public virtual T Get(long ID)
		{
			return dbSet.Find(ID);
		}

		public virtual T Get(Expression<Func<T, bool>> filter)
		{
			var entity = fetchData(filter, null, null, null, null).Single();
			return entity;
		}
		public virtual T Get(Expression<Func<T, bool>> filter, string[] includePaths = null)
		{
			var entity = fetchData(filter, includePaths, null, null, null).Single();
			return entity;
		}

		public async virtual Task<T> GetAsync(long ID)
		{
			var entity = await DbContext.Set<T>().FindAsync(ID);
			return entity;
		}
		public async virtual Task<T> GetAsync(Expression<Func<T, bool>> filter)
		{
			var entity = await fetchData(filter, null, null, null, null).SingleAsync();
			return entity;
		}
		public async virtual Task<T> GetAsync(Expression<Func<T, bool>> filter, string[] includePaths = null)
		{
			var entity = await fetchData(filter, includePaths, null, null, null).SingleAsync();
			return entity;
		}

		public virtual IEnumerable<T> GetAll()
		{
			return fetchData(null, null, null, null, null);
		}
		public virtual IEnumerable<T> GetAll(string[] includePaths = null)
		{
			return fetchData(null, includePaths, null, null, null);
		}
		public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
		{
			return fetchData(filter, null, null, null, null);
		}
		public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null)
		{
			return fetchData(filter, includePaths, null, null, null);
		}
		public virtual IPagedData<T> GetAll(int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return fetchData(null, null, page, pageSize, sortExpressions);
		}
		public virtual IPagedData<T> GetAll(string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return fetchData(null, includePaths, page, pageSize, sortExpressions);
		}
		public virtual IPagedData<T> GetAll(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, params SortExpression<T>[] sortExpressions)
		{
			return fetchData(filter, includePaths, page, pageSize, sortExpressions);
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await fetchData(null, null, null, null, null).ToListAsync();
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync(string[] includePaths = null)
		{
			return await fetchData(null, includePaths, null, null, null).ToListAsync();
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
		{
			return await fetchData(filter, null, null, null, null).ToListAsync();
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null)
		{
			return await fetchData(filter, includePaths, null, null, null).ToListAsync();
		}
		public virtual async Task<IPagedData<T>> GetAllAsync(int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return await fetchDataAsync(null, null, page, pageSize, sortExpressions);
		}
		public virtual async Task<IPagedData<T>> GetAllAsync(string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return await fetchDataAsync(null, includePaths, page, pageSize, sortExpressions);
		}
		public virtual async Task<IPagedData<T>> GetAllAsync(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, params SortExpression<T>[] sortExpressions)
		{
			return await fetchDataAsync(filter, includePaths, page, pageSize, sortExpressions);
		}

		private IQueryable<T> fetchData(Expression<Func<T, bool>> filter, string[] includePaths, params SortExpression<T>[] sortExpressions)
		{
			//DbContext.Configuration.LazyLoadingEnabled = false;
			IQueryable<T> query = dbSet;

			if (filter != null) query = query.Where(filter);

			if (includePaths != null && includePaths.Length > 0)
			{
				for (var i = 0; i < includePaths.Count(); i++)
				{
					query = query.Include(includePaths[i]);
				}
			}

			if (sortExpressions != null && sortExpressions.Length > 0)
			{
				IOrderedQueryable<T> orderedQuery = null;
				for (var i = 0; i < sortExpressions.Count(); i++)
				{
                    if (sortExpressions[i] !=null)
                    {
                        if (i == 0)
                        {
                            if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            {
                                orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
                            }
                            else
                            {
                                orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
                            }
                        }
                        else
                        {
                            if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            {
                                orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                            }
                            else
                            {
                                orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                            }
                        }
                    }
				}
			}
			return query;
		}

		private IPagedData<T> fetchData(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			//DbContext.Configuration.LazyLoadingEnabled = false;
			IQueryable<T> query = dbSet;

			int totalRecords = query.Count();

			if (filter != null) query = query.Where(filter);

			if (includePaths != null && includePaths.Length > 0)
			{
				for (var i = 0; i < includePaths.Count(); i++)
				{
					query = query.Include(includePaths[i]);
				}
			}
			if (pageSize > 0 && page < 0)
			{
				query = query.Take(pageSize);
			}
			if (page > 0)
			{
				query = query.Skip(page * pageSize).Take(pageSize);
			}

			var listing = query.ToList().AsQueryable();

			if (sortExpressions != null && sortExpressions.Length > 0)
			{
				IOrderedQueryable<T> orderedQuery = null;
				for (var i = 0; i < sortExpressions.Count(); i++)
				{
					if (i == 0)
					{
						if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
						{
							orderedQuery = listing.OrderBy(sortExpressions[i].SortBy);
						}
						else
						{
							orderedQuery = listing.OrderByDescending(sortExpressions[i].SortBy);
						}
					}
					else
					{
						if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
						{
							orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
						}
						else
						{
							orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
						}
					}
				}
				query = orderedQuery;
			}

			var data = query.ToList();
			return data.ToPagedList<T>(page, pageSize, totalRecords);

		}
		private async Task<IPagedData<T>> fetchDataAsync(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			//DbContext.Configuration.LazyLoadingEnabled = false;
			IQueryable<T> query = dbSet;

			int totalRecords = await query.CountAsync();

			if (filter != null) query = query.Where(filter);

			if (includePaths != null && includePaths.Length > 0)
			{
				for (var i = 0; i < includePaths.Count(); i++)
				{
					query = query.Include(includePaths[i]);
				}
			}

			if (sortExpressions != null && sortExpressions.Length > 0)
			{
				IOrderedQueryable<T> orderedQuery = null;
				for (var i = 0; i < sortExpressions.Count(); i++)
				{
					if (i == 0)
					{
						if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
						{
							orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
						}
						else
						{
							orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
						}
					}
					else
					{
						if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
						{
							orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
						}
						else
						{
							orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
						}
					}
				}
			}
			if (pageSize > 0)
			{
				query = query.Take(pageSize);
			}
			if (page > 0)
			{
				query = query.Skip(page * pageSize).Take(pageSize);
			}
			var data = await query.ToListAsync();
			return data.ToPagedList<T>(page, pageSize, totalRecords);
		}
		//private async Task<IPagedList<T>> fetchDataAsync(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		//{
		//	DbContext.Configuration.LazyLoadingEnabled = false;
		//	IQueryable<T> query = dbSet;

		//	int totalRecords = await dbSet.CountAsync();

		//	if (filter != null) query = query.Where(filter);

		//	if (includePaths != null && includePaths.Length > 0)
		//	{
		//		for (var i = 0; i < includePaths.Count(); i++)
		//		{
		//			query = query.Include(includePaths[i]);
		//		}
		//	}

		//	if (sortExpressions != null)
		//	{
		//		IOrderedQueryable<T> orderedQuery = null;
		//		for (var i = 0; i < sortExpressions.Count(); i++)
		//		{
		//			if (i == 0)
		//			{
		//				if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
		//				{
		//					orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
		//				}
		//				else
		//				{
		//					orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
		//				}
		//			}
		//			else
		//			{
		//				if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
		//				{
		//					orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
		//				}
		//				else
		//				{
		//					orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
		//				}
		//			}
		//		}
		//		query = orderedQuery;
		//	}
		//	if (page > 0)
		//	{
		//		query = query.Skip(((int)page - 1) * (int)pageSize);
		//	}
		//	if (pageSize > 0)
		//	{
		//		query = query.Take((int)pageSize);
		//	}
		//	var data = query.ToListAsync();

		//	return data.Result.ToPagedList(page, pageSize, totalRecords);
		//}
	}
}
