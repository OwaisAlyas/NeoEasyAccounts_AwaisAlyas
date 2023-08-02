using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedAds.Model.Admin
{
	public class Role : IdentityRole<long, UserRole>, IRole<long>
	{
		public Role()
		{ }
		public Role(string name)
			: this()
		{
			this.Name = name;
		}

		public Role(string name, string description)
			: this(name)
		{
			this.Description = description;
		}

		public string Description { get; set; }
	}
}
