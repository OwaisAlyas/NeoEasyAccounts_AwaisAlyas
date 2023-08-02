using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public class ReceiptVoucher : VoucherBase
	{
		public long CustomerID { get; set; }
		public virtual Customer Customer { get; set; }

		public virtual ICollection<ReceiptVoucherDetail> ReceiptVoucherDetails { get; set; }
	}
}
