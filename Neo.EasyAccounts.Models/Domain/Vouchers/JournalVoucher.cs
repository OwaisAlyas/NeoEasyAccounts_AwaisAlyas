using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public class JournalVoucher : VoucherBase
	{
		public virtual ICollection<JournalVoucherDetail> JournalVoucherDetails { get; set; }
	}
}
