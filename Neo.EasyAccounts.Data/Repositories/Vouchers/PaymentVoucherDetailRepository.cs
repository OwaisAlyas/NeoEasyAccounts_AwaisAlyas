using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Vouchers
{
	public interface IPaymentVoucherDetailRepository : IRepository<PaymentVoucherDetail>
	{
		IEnumerable<PaymentVoucherDetail> GetAll(long journalVoucherID);
	}

	internal class PaymentVoucherDetailRepository : RepositoryBase<PaymentVoucherDetail>, IPaymentVoucherDetailRepository
	{
		public PaymentVoucherDetailRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}
		
		public IEnumerable<PaymentVoucherDetail> GetAll(long journalVoucherID)
		{
			var list = base.DbContext.PaymentVoucherDetails.Where(d => d.PaymentVoucherID == journalVoucherID).ToList();
			return list;
		}
	}
}
