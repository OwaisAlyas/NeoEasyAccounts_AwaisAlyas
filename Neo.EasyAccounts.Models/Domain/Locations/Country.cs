using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Locations
{
	public class Country : LocationBase
	{
		public virtual ICollection<State> States { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

}
