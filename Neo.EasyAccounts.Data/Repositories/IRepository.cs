using Neo.Common.Data.PagedData;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories
{
	public interface IRepository<T> where T : class
	{
		T Insert(T entity);
		T Update(T entity);
		T Delete(T entity);
		void Reload(T entity);
		void ReloadAsync(T model);

		T Get(long ID);
		T Get(Expression<Func<T, bool>> filter);
		T Get(Expression<Func<T, bool>> filter, string[] includePaths = null);

		Task<T> GetAsync(long ID);
		Task<T> GetAsync(Expression<Func<T, bool>> filter);
		Task<T> GetAsync(Expression<Func<T, bool>> filter, string[] includePaths = null);

		IEnumerable<T> GetAll();
		IEnumerable<T> GetAll(string[] includePaths = null);
		IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
		IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null);
		IPagedData<T> GetAll(int page, int pageSize, SortExpression<T>[] sortExpressions);
		IPagedData<T> GetAll(string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions);
		IPagedData<T> GetAll(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions);

		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllAsync(string[] includePaths = null);
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null);
		Task<IPagedData<T>> GetAllAsync(int page, int pageSize, SortExpression<T>[] sortExpressions);
		Task<IPagedData<T>> GetAllAsync(string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions);
		Task<IPagedData<T>> GetAllAsync(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions);
	}
}


//public interface IRepository<T> where T : class
//{
//	T Insert(T entity);
//	T Update(T entity);
//	T Delete(T entity);
//	void Reload(T entity);
//	void ReloadAsync(T model);

//	T Get(long ID);
//	T Get(Expression<Func<T, bool>> filter);
//	T Get(Expression<Func<T, bool>> filter, string[] includePaths = null);

//	Task<T> GetAsync(long ID);
//	Task<T> GetAsync(Expression<Func<T, bool>> filter);
//	Task<T> GetAsync(Expression<Func<T, bool>> filter, string[] includePaths = null);

//	IEnumerable<T> GetAll();
//	IEnumerable<T> GetAll(string[] includePaths = null);
//	IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
//	IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null);
//	IPagedList<T> GetAll(int? page = 0, int? pageSize = null, SortExpression<T>[] sortExpressions = null);
//	IPagedList<T> GetAll(string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);
//	IPagedList<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);

//	Task<IEnumerable<T>> GetAllAsync();
//	Task<IEnumerable<T>> GetAllAsync(string[] includePaths = null);
//	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
//	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null);
//	Task<IPagedList<T>> GetAllAsync(int? page = 0, int? pageSize = null, SortExpression<T>[] sortExpressions = null);
//	Task<IPagedList<T>> GetAllAsync(string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);
//	Task<IPagedList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);
//}