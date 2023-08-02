using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public abstract class VoucherDetailBase : AuditableEntity//<long>
	{
		//public long VoucherID { get; set; }
		public long AccountID { get; set; }
		public Nullable<decimal> Debit { get; set; }
		public Nullable<decimal> Credit { get; set; }
		public string Narration { get; set; }

		//public virtual VoucherBase Voucher { get; set; }
		public virtual Accounts.AccountTitle Account { get; set; }
	}
}
