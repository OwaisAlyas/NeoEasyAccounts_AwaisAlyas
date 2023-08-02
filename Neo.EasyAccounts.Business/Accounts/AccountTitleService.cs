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
	public interface IAccountTitleService : IService<AccountTitle>
	{
		AccountTitle Get(string code);
		IEnumerable<AccountTitle> GetAll(string code);
		AccountTitle GetByAccountSubGroup(long accountSubGroupID);
		IEnumerable<AccountTitle> GetAllByAccountSubGroup(long accountSubGroupID);

		Task<AccountTitle> GetAsync(string code);
		Task<IEnumerable<AccountTitle>> GetAllAsync(string code);
		Task<AccountTitle> GetByAccountSubGroupAsync(long accountSubGroupID);
		Task<IEnumerable<AccountTitle>> GetAllByAccountSubGroupAsync(long accountSubGroupID);
	}
	internal class AccountTitleService : ServiceBase<AccountTitle>, IAccountTitleService
	{
		private readonly IAccountTitleRepository repo;

		public AccountTitleService(IAccountTitleRepository repo, IRepository<AccountTitle> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public AccountTitle Get(string code)
		{
			var entity = repo.Get(d => d.Code == code);
			return entity;
		}
		public IEnumerable<AccountTitle> GetAll(string code)
		{
			var list = repo.GetAll(d => d.Code == code);
			return list;
		}
		public AccountTitle GetByAccountSubGroup(long accountSubGroupID)
		{
			var entity = repo.Get(d => d.AccountSubGroupID == accountSubGroupID);
			return entity;
		}
		public IEnumerable<AccountTitle> GetAllByAccountSubGroup(long accountSubGroupID)
		{
			var list = repo.GetAll(d => d.AccountSubGroupID == accountSubGroupID);
			return list;
		}

		public async Task<AccountTitle> GetAsync(string code)
		{
			var entity = await repo.GetAsync(d => d.Code == code);
			return entity;
		}
		public async Task<IEnumerable<AccountTitle>> GetAllAsync(string code)
		{
			var list = await repo.GetAllAsync(d => d.Code == code);
			return list;
		}
		public async Task<AccountTitle> GetByAccountSubGroupAsync(long accountSubGroupID)
		{
			var entity = await repo.GetAsync(d => d.AccountSubGroupID == accountSubGroupID);
			return entity;
		}
		public async Task<IEnumerable<AccountTitle>> GetAllByAccountSubGroupAsync(long accountSubGroupID)
		{
			var list = await repo.GetAllAsync(d => d.AccountSubGroupID == accountSubGroupID);
			return list;
		}
	}
}
