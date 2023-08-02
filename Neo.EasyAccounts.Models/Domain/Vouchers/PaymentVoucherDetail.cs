using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public class PaymentVoucherDetail : VoucherDetailBase
	{
		public string ChequeNo { get; set; }

		public long PaymentVoucherID { get; set; }
		public virtual PaymentVoucher PaymentVoucher { get; set; }
	}
}
