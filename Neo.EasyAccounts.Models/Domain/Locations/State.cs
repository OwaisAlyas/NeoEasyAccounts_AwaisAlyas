using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Locations
{
	public class State : LocationBase
	{
		public long CountryID { get; set; }
		public virtual Country Country { get; set; }
		public virtual ICollection<City> Cities { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
