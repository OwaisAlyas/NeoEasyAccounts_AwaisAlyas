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
	public interface IAccountSubGroupService : IService<AccountSubGroup>
	{
		AccountSubGroup Get(string code);
		IEnumerable<AccountSubGroup> GetAll(string code);
		AccountSubGroup GetByAccountSubGroup(long accountTypeID);
		IEnumerable<AccountSubGroup> GetAllByAccountSubGroup(long accountTypeID);

		Task<AccountSubGroup> GetAsync(string code);
		Task<IEnumerable<AccountSubGroup>> GetAllAsync(string code);
		Task<AccountSubGroup> GetByAccountSubGroupAsync(long accountTypeID);
		Task<IEnumerable<AccountSubGroup>> GetAllByAccountSubGroupAsync(long accountTypeID);
	}
	internal class AccountSubGroupService : ServiceBase<AccountSubGroup>, IAccountSubGroupService
	{
		private readonly IAccountSubGroupRepository repo;
		public AccountSubGroupService(IAccountSubGroupRepository repo, IRepository<AccountSubGroup> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public AccountSubGroup Get(string code)
		{
			var entity = repo.Get(d => d.Code == code);
			return entity;
		}
		public IEnumerable<AccountSubGroup> GetAll(string code)
		{
			var list = repo.GetAll(d => d.Code == code);
			return list;
		}
		public AccountSubGroup GetByAccountSubGroup(long accountTypeID)
		{
			var entity = repo.Get(d => d.AccountGroupID == accountTypeID);
			return entity;
		}
		public IEnumerable<AccountSubGroup> GetAllByAccountSubGroup(long accountTypeID)
		{
			var list = repo.GetAll(d => d.AccountGroupID == accountTypeID);
			return list;
		}

		public async Task<AccountSubGroup> GetAsync(string code)
		{
			var entity = await repo.GetAsync(d => d.Code == code);
			return entity;
		}
		public async Task<IEnumerable<AccountSubGroup>> GetAllAsync(string code)
		{
			var list = await repo.GetAllAsync(d => d.Code == code);
			return list;
		}
		public async Task<AccountSubGroup> GetByAccountSubGroupAsync(long accountTypeID)
		{
			var entity = await repo.GetAsync(d => d.AccountGroupID == accountTypeID);
			return entity;
		}
		public async Task<IEnumerable<AccountSubGroup>> GetAllByAccountSubGroupAsync(long accountTypeID)
		{
			var list = await repo.GetAllAsync(d => d.AccountGroupID == accountTypeID);
			return list;
		}
	}
}
