using Neo.EasyAccounts.Models.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Accounts
{
	public interface IAccountTypeRepository : IRepository<AccountType>
	{

	}

	internal class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
	{
		public AccountTypeRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
