using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain
{
	public class Supplier : PersonBase
	{
		public string Description { get; set; }

		public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
	}
}
