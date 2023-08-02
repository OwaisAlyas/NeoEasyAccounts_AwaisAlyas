using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Vouchers
{
	public abstract class VoucherBase : AuditableEntity//<long>
	{
		public string Number { get; set; }
		public DateTime Date { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
	}
}
