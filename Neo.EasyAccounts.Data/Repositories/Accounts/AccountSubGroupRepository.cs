using Neo.EasyAccounts.Models.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Accounts
{
	public interface IAccountSubGroupRepository : IRepository<AccountSubGroup>
	{
	}

	internal class AccountSubGroupRepository : RepositoryBase<AccountSubGroup>, IAccountSubGroupRepository
	{
		public AccountSubGroupRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
