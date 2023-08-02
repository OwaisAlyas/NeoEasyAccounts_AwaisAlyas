using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Repositories.Vouchers
{
	public interface IReceiptVoucherDetailRepository : IRepository<ReceiptVoucherDetail>
	{
		IEnumerable<ReceiptVoucherDetail> GetAll(long receiptVoucherID);
	}

	internal class ReceiptVoucherDetailRepository : RepositoryBase<ReceiptVoucherDetail>, IReceiptVoucherDetailRepository
	{
		public ReceiptVoucherDetailRepository(IDbFactory dbFactory)
			: base(dbFactory)
		{

		}

		public IEnumerable<ReceiptVoucherDetail> GetAll(long receiptVoucherID)
		{
			var list = base.DbContext.ReceiptVoucherDetails.Where(d => d.ReceiptVoucherID == receiptVoucherID).ToList();
			return list;
		}
	}
}
