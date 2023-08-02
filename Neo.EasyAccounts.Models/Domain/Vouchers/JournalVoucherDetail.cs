using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public class JournalVoucherDetail : VoucherDetailBase
	{
		public long JournalVoucherID { get; set; }
		public virtual JournalVoucher JournalVoucher { get; set; }
	}
}
