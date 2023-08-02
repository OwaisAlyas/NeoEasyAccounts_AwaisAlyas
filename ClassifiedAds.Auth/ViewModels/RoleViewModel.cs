using ClassifiedAds.Model.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Auth.ViewModels
{
	public class RoleViewModel
	{
		public RoleViewModel()
		{
		}
		public RoleViewModel(Role role)
		{
			this.RoleName = role.Name;
			this.Description = role.Description;
			this.ID = role.Id;
			this.TotalUserCount = role.Users.Count;
		}

		public long ID { get; set; }
		[Required]
		[Display(Name = "Role Name")]
		public string RoleName { get; set; }
		public string Description { get; set; }

		[Display(Name = "User Count")]
		[ScaffoldColumn(false)]
		public int TotalUserCount { get; set; }

	}
}
