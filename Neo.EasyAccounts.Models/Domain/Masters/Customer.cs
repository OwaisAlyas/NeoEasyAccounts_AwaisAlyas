using Neo.EasyAccounts.Models.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain
{
	public class Customer : PersonBase
	{
		public virtual ICollection<ReceiptVoucher> ReceiptVouches { get; set; }
	}
}
