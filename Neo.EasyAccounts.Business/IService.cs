using Neo.Common.Data.PagedData;
using Neo.EasyAccounts.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service
{
	public interface IService<T> where T : class
	{
		T Save(T entity);
		IEnumerable<T> Save(IEnumerable<T> entities);
		int Delete(long id, bool isPremanent = false);
		int Delete(T entity, bool isPremanent = false);
		int Delete(IEnumerable<T> entities, bool isPremanent = false);

		Task<T> SaveAsync(T entity);
		Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
		Task<int> DeleteAsync(long id, bool isPremanent = false);
		Task<int> DeleteAsync(T entity, bool isPremanent = false);
		Task<int> DeleteAsync(IEnumerable<T> entities, bool isPremanent = false);

		void Refresh(T entity);

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

//public interface IService<T> where T : class
//{
//	T Save(T entity);
//	IEnumerable<T> Save(IEnumerable<T> entities);
//	int Delete(long id, bool isPremanent = false);
//	int Delete(T entity, bool isPremanent = false);
//	int Delete(IEnumerable<T> entities, bool isPremanent = false);

//	Task<T> SaveAsync(T entity);
//	Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
//	Task<int> DeleteAsync(long id, bool isPremanent = false);
//	Task<int> DeleteAsync(T entity, bool isPremanent = false);
//	Task<int> DeleteAsync(IEnumerable<T> entities, bool isPremanent = false);

//	void Refresh(T entity);

//	T Get(long ID);
//	T Get(Expression<Func<T, bool>> filter);
//	T Get(Expression<Func<T, bool>> filter, string[] includePaths = null);

//	Task<T> GetAsync(long ID);
//	Task<T> GetAsync(Expression<Func<T, bool>> filter);
//	Task<T> GetAsync(Expression<Func<T, bool>> filter, string[] includePaths = null);

//	IEnumerable<T> GetAll();
//	IEnumerable<T> GetAll(string[] includePaths = null);
//	IEnumerable<T> GetAll(int? page = 0, int? pageSize = null, SortExpression<T>[] sortExpressions = null);
//	IEnumerable<T> GetAll(string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);
//	IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
//	IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null);
//	IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);

//	Task<IEnumerable<T>> GetAllAsync();
//	Task<IEnumerable<T>> GetAllAsync(string[] includePaths = null);
//	Task<IEnumerable<T>> GetAllAsync(int? page = 0, int? pageSize = null, SortExpression<T>[] sortExpressions = null);
//	Task<IEnumerable<T>> GetAllAsync(string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);
//	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
//	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null);
//	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null, int? page = 0, int? pageSize = null, params SortExpression<T>[] sortExpressions);
//}