﻿using Neo.EasyAccounts.Models.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Accounts
{
	public interface IAccountTitleRepository : IRepository<AccountTitle>
	{
	}

	internal class AccountTitleRepository : RepositoryBase<AccountTitle>, IAccountTitleRepository
	{
		public AccountTitleRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
