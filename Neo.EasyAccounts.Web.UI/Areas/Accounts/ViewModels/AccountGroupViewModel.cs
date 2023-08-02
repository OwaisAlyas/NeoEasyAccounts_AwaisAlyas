
namespace Neo.EasyAccounts.Web.UI.Areas.Accounts.ViewModels
{
	using Neo.EasyAccounts.Models.Domain.Accounts;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;
	public class AccountGroupViewModel
	{
		public long ID { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Account Type")]
		public long AccountTypeID { get; set; }

		public SelectList AccountTypes { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[StringLength(250, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 3)]
		public string Name { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[StringLength(250, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 3)]
		public string Code { get; set; }

		public string Description { get; set; }

		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateModified { get; set; }

		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }

		public IEnumerable<AccountSubGroup> SubGroupListing { get; set; }
	}
}