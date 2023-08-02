using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Models.Domain.Locations
{
	public class City : LocationBase
	{
		public long StateID { get; set; }
		public virtual State State { get; set; }
		public virtual ICollection<Area> Areas { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
