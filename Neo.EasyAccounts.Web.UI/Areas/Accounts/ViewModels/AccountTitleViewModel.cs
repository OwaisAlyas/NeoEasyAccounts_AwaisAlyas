﻿
namespace Neo.EasyAccounts.Web.UI.Areas.Accounts.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class AccountTitleViewModel
	{
		public long ID { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Account Type")]
		public long AccountTypeID { get; set; }
		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Account Group")]
		public long AccountGroupID { get; set; }
		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Account SubGroup")]
		public long AccountSubGroupID { get; set; }

		public SelectList AccounTypes { get; set; }
		public SelectList AccountGroups { get; set; }
		public SelectList AccountSubGroups { get; set; }

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
	}
}