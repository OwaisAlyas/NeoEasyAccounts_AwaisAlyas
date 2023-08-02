using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Neo.EasyAccounts.Data.Repositories.Vouchers
{
	public interface IJournalVoucherRepository : IRepository<JournalVoucher>
	{
	}

	internal class JournalVoucherRepository : RepositoryBase<JournalVoucher>, IJournalVoucherRepository
	{
		public JournalVoucherRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
