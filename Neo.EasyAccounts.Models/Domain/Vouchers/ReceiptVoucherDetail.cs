using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public class ReceiptVoucherDetail : VoucherDetailBase
	{
		public string ChequeNo { get; set; }

		public long ReceiptVoucherID { get; set; }
		public virtual ReceiptVoucher ReceiptVoucher { get; set; }
	}
}
