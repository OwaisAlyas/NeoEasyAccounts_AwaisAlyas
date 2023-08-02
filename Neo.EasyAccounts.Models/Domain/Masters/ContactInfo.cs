using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain
{
	public class ContactInfo : AuditableEntity//<long>
	{
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public bool IsPhoneDefault { get; set; }
		public bool IsFaxDefault { get; set; }
		public bool IsEmailDefault { get; set; }

		public Nullable<long> CompanyID { get; set; }
		public Nullable<long> CustomerID { get; set; }
		public Nullable<long> SupplierID { get; set; }
		
		public virtual Company Company { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual Supplier Supplier { get; set; }
	}
}
