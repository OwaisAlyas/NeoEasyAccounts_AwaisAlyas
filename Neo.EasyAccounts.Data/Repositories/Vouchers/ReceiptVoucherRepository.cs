using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Vouchers
{
	public interface IReceiptVoucherRepository : IRepository<ReceiptVoucher>
	{
		
	}

	internal class ReceiptVoucherRepository : RepositoryBase<ReceiptVoucher>, IReceiptVoucherRepository
	{
		public ReceiptVoucherRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
		
	}
}
