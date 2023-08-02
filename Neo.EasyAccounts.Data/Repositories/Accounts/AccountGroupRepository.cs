using Neo.EasyAccounts.Models.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Accounts
{
	public interface IAccountGroupRepository : IRepository<AccountGroup>
	{
	}

	internal class AccountGroupRepository : RepositoryBase<AccountGroup>, IAccountGroupRepository
	{
		public AccountGroupRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
