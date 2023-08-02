using Neo.Common.Data.PagedData;
using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service
{
	public abstract class ServiceBase<T> : IService<T> where T : class
	{
		private IRepository<T> repo;
		private IUnitofWork unitofWork;

		public int AffectedRows { get; private set; }

		public ServiceBase(IRepository<T> repo, IUnitofWork unitofWork)
		{
			this.repo = repo;
			this.unitofWork = unitofWork;
			AffectedRows = 0;
		}

		public virtual T Add(T model)
		{
			repo.Insert(model);
			int affectedRows = unitofWork.Commit();
			AffectedRows = affectedRows;
			return model;
		}
		public virtual T Update(T model)
		{
			repo.Update(model);
			int affectedRows = unitofWork.Commit();
			AffectedRows = affectedRows;
			return model;
		}
		public virtual int Delete(T model)
		{
			repo.Delete(model);
			int affectedRows = unitofWork.Commit();
			AffectedRows = affectedRows;
			return affectedRows;
		}
		public virtual void Refresh(T model)
		{
			repo.ReloadAsync(model);
		}

		public async virtual Task<T> UpdateAsync(T model)
		{
			repo.Update(model);
			int affectedRows = await unitofWork.CommitAsync();
			AffectedRows = affectedRows;
			return model;
		}
		public async virtual Task<T> AddAsync(T model)
		{
			repo.Insert(model);
			int affectedRows = await unitofWork.CommitAsync();
			AffectedRows = affectedRows;
			return model;
		}
		public async virtual Task<int> DeleteAsync(T model)
		{
			repo.Delete(model);
			int affectedRows = await unitofWork.CommitAsync();
			AffectedRows = affectedRows;
			return affectedRows;
		}

		public virtual T Save(T entity)
		{
			//if (entity is EasyAccounts.Models.Domain.Entity<long>)
			//{
			//	var modelEntity = entity as EasyAccounts.Models.Domain.Entity<long>;

			//	return modelEntity.ID > 0 ? Update(entity) : Add(entity);
			//}
			if (entity is EasyAccounts.Models.Domain.Entity)
			{
				var modelEntity = entity as EasyAccounts.Models.Domain.Entity;

				return modelEntity.ID > 0 ? Update(entity) : Add(entity);
			}
			return null;
		}
		public virtual IEnumerable<T> Save(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				//if (entity is EasyAccounts.Models.Domain.Entity<long>)
				if (entity is EasyAccounts.Models.Domain.Entity)
				{
					var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;

					if (modelEntity.ID > 0)
						repo.Update(entity);
					else
						repo.Insert(entity);
				}
			}
			unitofWork.Commit();
			return entities;
		}

		public virtual int Delete(long id, bool isPremanent = false)
		{
			if (id < 0) return -1;

			var entity = Get(id);

			if (entity == null) return -2;

			int output = Delete(entity, isPremanent);

			return output;
		}
		public virtual int Delete(T entity, bool isPremanent = false)
		{
			int output = 0;

			if (isPremanent)
			{
				output = Delete(entity);
			}
			else
			{
				if (entity is EasyAccounts.Models.Domain.Entity)//<long>)
				{
					var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;
					modelEntity.IsDeleted = true;
					Update(entity);
					output = AffectedRows;
				}
			}
			return output;
		}
		public virtual int Delete(IEnumerable<T> entities, bool isPremanent = false)
		{
			if (entities == null) return -1;
			if (entities != null && entities.Count() < 1) return 0;

			foreach (var entity in entities)
			{
				if (isPremanent)
				{
					repo.Delete(entity);
				}
				else
				{
					if (entity is EasyAccounts.Models.Domain.Entity)//<long>)
					{
						var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;
						modelEntity.IsDeleted = true;
						repo.Update(entity);
					}
				}
			}
			int output = unitofWork.Commit();
			return output;
		}

		public async virtual Task<T> SaveAsync(T entity)
		{
			if (entity is EasyAccounts.Models.Domain.Entity)//<long>)
			{
				var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;

				return modelEntity.ID > 0 ? await UpdateAsync(entity) : await AddAsync(entity);
			}
			return null;
		}
		public async virtual Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				if (entity is EasyAccounts.Models.Domain.Entity)//<long>)
				{
					var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;

					if (modelEntity.ID > 0)
						repo.Update(entity);
					else
						repo.Insert(entity);
				}
			}
			await unitofWork.CommitAsync();
			return entities;
		}
		public async virtual Task<int> DeleteAsync(long id, bool isPremanent = false)
		{
			if (id < 0) return -1;

			var entity = Get(id);

			if (entity == null) return -2;

			int output = await DeleteAsync(entity, isPremanent);

			return output;
		}
		public async virtual Task<int> DeleteAsync(T entity, bool isPremanent = false)
		{
			int output = 0;

			if (isPremanent)
			{
				output = await DeleteAsync(entity);
			}
			else
			{
				if (entity is EasyAccounts.Models.Domain.Entity)//<long>)
				{
					var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;
					modelEntity.IsDeleted = true;
					await UpdateAsync(entity);
					output = AffectedRows;
				}
			}
			return output;
		}
		public async virtual Task<int> DeleteAsync(IEnumerable<T> entities, bool isPremanent = false)
		{
			if (entities == null) return -1;
			if (entities != null && entities.Count() < 1) return 0;

			foreach (var entity in entities)
			{
				if (isPremanent)
				{
					repo.Delete(entity);
				}
				else
				{
					if (entity is EasyAccounts.Models.Domain.Entity)//<long>)
					{
						var modelEntity = entity as EasyAccounts.Models.Domain.Entity;//<long>;
						modelEntity.IsDeleted = true;
						repo.Update(entity);
					}
				}
			}
			int output = await unitofWork.CommitAsync();
			return output;
		}

		public virtual T Get(long ID)
		{
			return repo.Get(ID);
		}

		public virtual T Get(Expression<Func<T, bool>> condition)
		{
			var entity = repo.Get(condition);
			return entity;
		}
		public virtual T Get(Expression<Func<T, bool>> condition, string[] includePaths = null)
		{
			var entity = repo.Get(condition, includePaths);
			return entity;
		}

		public async virtual Task<T> GetAsync(long ID)
		{
			var entity = await repo.GetAsync(ID);
			return entity;
		}
		public async virtual Task<T> GetAsync(Expression<Func<T, bool>> condition)
		{
			var entity = await repo.GetAsync(condition);
			return entity;
		}
		public async virtual Task<T> GetAsync(Expression<Func<T, bool>> condition, string[] includePaths = null)
		{
			var entity = await repo.GetAsync(condition, includePaths);
			return entity;
		}

		public virtual IEnumerable<T> GetAll()
		{
			return repo.GetAll();
		}
		public virtual IEnumerable<T> GetAll(string[] includePaths = null)
		{
			return repo.GetAll(includePaths);
		}
		public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
		{
			return repo.GetAll(filter);
		}
		public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string[] includePaths = null)
		{
			return repo.GetAll(filter, includePaths);
		}
		public virtual IPagedData<T> GetAll(int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return repo.GetAll(page, pageSize, sortExpressions);
		}
		public virtual IPagedData<T> GetAll(string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return repo.GetAll(includePaths, page, pageSize, sortExpressions);
		}
		public virtual IPagedData<T> GetAll(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return repo.GetAll(filter, includePaths, page, pageSize, sortExpressions);
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await repo.GetAllAsync();
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync(string[] includePaths = null)
		{
			return await repo.GetAllAsync(includePaths);
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
		{
			return await repo.GetAllAsync(filter);
		}
		public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string[] includePaths = null)
		{
			return await repo.GetAllAsync(filter, includePaths);
		}
		public virtual async Task<IPagedData<T>> GetAllAsync(int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return await repo.GetAllAsync(page, pageSize, sortExpressions);
		}
		public virtual async Task<IPagedData<T>> GetAllAsync(string[] includePaths, int page, int pageSize, params SortExpression<T>[] sortExpressions)
		{
			return await repo.GetAllAsync(includePaths, page, pageSize, sortExpressions);
		}
		public virtual async Task<IPagedData<T>> GetAllAsync(Expression<Func<T, bool>> filter, string[] includePaths, int page, int pageSize, SortExpression<T>[] sortExpressions)
		{
			return await repo.GetAllAsync(filter, includePaths, page, pageSize, sortExpressions);
		}
	}
}