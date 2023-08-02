using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Repositories;
using Neo.EasyAccounts.Data.Repositories.Accounts;
using Neo.EasyAccounts.Models.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service.Accounts
{
	public interface IAccountGroupService : IService<AccountGroup>
	{
		AccountGroup Get(string code);
		IEnumerable<AccountGroup> GetAll(string code);
		AccountGroup GetByAccountType(long accountTypeID);
		IEnumerable<AccountGroup> GetAllByAccountType(long accountTypeID);

		Task<AccountGroup> GetAsync(string code);
		Task<IEnumerable<AccountGroup>> GetAllAsync(string code);
		Task<AccountGroup> GetByAccountTypeAsync(long accountTypeID);
		Task<IEnumerable<AccountGroup>> GetAllByAccountTypeAsync(long accountTypeID);
	}
	internal class AccountGroupService : ServiceBase<AccountGroup>, IAccountGroupService
	{
		private readonly IAccountGroupRepository repo;
		public AccountGroupService(IAccountGroupRepository repo, IRepository<AccountGroup> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public AccountGroup GetByAccountType(long accountTypeID)
		{
			var entity = repo.Get(d => d.AccountTypeID == accountTypeID);
			return entity;
		}
		public IEnumerable<AccountGroup> GetAllByAccountType(long accountTypeID)
		{
			var entities = repo.GetAll(d => d.AccountTypeID == accountTypeID);
			return entities;
		}
		public AccountGroup Get(string code)
		{
			var entity = repo.Get(d => d.Code.Equals(code));
			return entity;
		}
		public IEnumerable<AccountGroup> GetAll(string code)
		{
			var entities = repo.GetAll(d => d.Code.Equals(code));
			return entities;
		}

		public async Task<AccountGroup> GetAsync(string code)
		{
			var entity = await repo.GetAsync(d => d.Code.Equals(code));
			return entity;
		}
		public async Task<IEnumerable<AccountGroup>> GetAllAsync(string code)
		{
			var entities = await repo.GetAllAsync(d => d.Code.Equals(code));
			return entities;
		}
		public async Task<AccountGroup> GetByAccountTypeAsync(long accountTypeID)
		{
			var entity = await repo.GetAsync(d => d.AccountTypeID == accountTypeID);
			return entity;
		}
		public async Task<IEnumerable<AccountGroup>> GetAllByAccountTypeAsync(long accountTypeID)
		{
			var entities = await repo.GetAllAsync(d => d.AccountTypeID == accountTypeID);
			return entities;
		}
	}
}
