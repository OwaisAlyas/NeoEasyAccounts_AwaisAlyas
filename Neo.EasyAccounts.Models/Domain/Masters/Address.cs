using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain
{
	public class Address : AuditableEntity//<long>
	{
		public string Street { get; set; }
		public string Building { get; set; }
		public string Floor { get; set; }
		public string OfficeNo { get; set; }
		public string HouseNo { get; set; }
		public string FlatNo { get; set; }
		public string POBoxNo { get; set; }

		public Nullable<long> CountryID { get; set; }
		public Nullable<long> CityID { get; set; }
		public Nullable<long> AreaID { get; set; }
		public Nullable<long> CompanyID { get; set; }
		public Nullable<long> CustomerID { get; set; }
		public Nullable<long> SupplierID { get; set; }
		public Nullable<long> EmployeeID { get; set; }
		public Nullable<long> LineOfBusinessID { get; set; }

		public virtual Locations.Country Country { get; set; }
		public virtual Locations.City City { get; set; }
		public virtual Locations.Area Area { get; set; }
		public virtual Company Company { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual Supplier Supplier { get; set; }
		public virtual Employee Employee { get; set; }
		public virtual LineOfBusiness LineOfBusiness { get; set; }
	}
}
