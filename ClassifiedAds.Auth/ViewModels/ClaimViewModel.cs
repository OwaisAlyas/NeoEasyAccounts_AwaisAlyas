using ClassifiedAds.Model.Admin;
using System.ComponentModel.DataAnnotations;

namespace Neo.Auth.ViewModels
{
	public class ClaimViewModel
	{
		public ClaimViewModel()
		{
		}
		public ClaimViewModel(ApplicationClaim claim)
		{
			this.Name = claim.Name;
			this.Description = claim.Description;
			this.Id = claim.Id;
		}

		public long Id { get; set; }
		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Claim Name")]
		public string Name { get; set; }

		[Display(Name = "Claim Value")]
		public string Value { get; set; }
		public string Description { get; set; }
	}
}
