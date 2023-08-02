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
	public interface IAccountTypeService : IService<AccountType>
	{
		AccountType Get(string code);
		IEnumerable<AccountType> GetAll(string code);

		Task<AccountType> GetAsync(string code);
		Task<IEnumerable<AccountType>> GetAllAsync(string code);
	}
	internal class AccountTypeService : ServiceBase<AccountType>, IAccountTypeService
	{
		private readonly IAccountTypeRepository repo;
		public AccountTypeService(IAccountTypeRepository repo ,IRepository<AccountType> repository, IUnitofWork unitofWork)
			: base(repository, unitofWork)
		{
			this.repo = repo;
		}

		public AccountType Get(string code)
		{
			var entity = repo.Get(d => d.Code.Equals(code));
			return entity;
		}
		public IEnumerable<AccountType> GetAll(string code)
		{
			var entities = repo.GetAll(d => d.Code.Equals(code));
			return entities;
		}

		public async Task<AccountType> GetAsync(string code)
		{
			var entity = await repo.GetAsync(d => d.Code.Equals(code));
			return entity;
		}
		public async Task<IEnumerable<AccountType>> GetAllAsync(string code)
		{
			var entities = await repo.GetAllAsync(d => d.Code.Equals(code));
			return entities;
		}
	}
}
