using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Accounts
{
	public class AccountGroup : AccountBase
	{
		public long AccountTypeID { get; set; }
		public virtual AccountType AccountType { get; set; }

		public virtual ICollection<AccountSubGroup> AccountSubGroups { get; set; }
	}
}
