using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Vouchers
{
	public interface IJournalVoucherDetailRepository : IRepository<JournalVoucherDetail>
	{
		IEnumerable<JournalVoucherDetail> GetAll(long journalVoucherID);
	}

	internal class JournalVoucherDetailRepository : RepositoryBase<JournalVoucherDetail>, IJournalVoucherDetailRepository
	{
		public JournalVoucherDetailRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public IEnumerable<JournalVoucherDetail> GetAll(long journalVoucherID)
		{
			var list = base.DbContext.JournalVoucherDetails.Where(d => d.JournalVoucherID == journalVoucherID).ToList();
			return list;
		}
	}
}
