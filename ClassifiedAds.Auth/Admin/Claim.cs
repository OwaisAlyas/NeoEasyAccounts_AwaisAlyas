using Neo.Auth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedAds.Model.Admin
{
	public class ApplicationClaim
	{
		public ApplicationClaim()
		{

		}
		public ApplicationClaim(ClaimViewModel viewModel)
		{
			this.Id = viewModel.Id;
			this.Name = viewModel.Name;
			this.Description = viewModel.Description;
		}

		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
