using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public class PaymentVoucher : VoucherBase
	{
		public long SupplierID { get; set; }
		public virtual Supplier Supplier { get; set; }

		public virtual ICollection<PaymentVoucherDetail> PaymentVoucherDetails { get; set; }
	}
}
