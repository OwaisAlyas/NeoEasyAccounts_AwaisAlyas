
namespace Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class JournalVoucherDetailViewModel
	{
		public long ID { get; set; }

		//[Required(ErrorMessage = "{0} Required")]
		public long JournalVoucherID { get; set; }
		
		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Account")]
		public long AccountID { get; set; }
		public Nullable<decimal> Debit { get; set; }
		public Nullable<decimal> Credit { get; set; }

		[StringLength(500, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 5)]
		public string Narration { get; set; }

		//public virtual SelectList Accounts { get; set; }
		
		//public long CreatedBy { get; set; }
		//public long? ModifiedBy { get; set; }
		//public DateTime DateCreated { get; set; }
		//public DateTime? DateModified { get; set; }

		//public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
	}
}