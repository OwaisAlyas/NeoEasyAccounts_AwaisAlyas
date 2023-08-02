using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Vouchers
{
	public interface IPaymentVoucherRepository : IRepository<PaymentVoucher>
	{
		
	}

	internal class PaymentVoucherRepository : RepositoryBase<PaymentVoucher>, IPaymentVoucherRepository
	{
		public PaymentVoucherRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
	}
}
