
namespace Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ReceiptVoucherDetailViewModel
	{
		public long ID { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		public long VoucherID { get; set; }

		public long AccountID { get; set; }
		public Nullable<decimal> Debit { get; set; }
		public Nullable<decimal> Credit { get; set; }

		[StringLength(500, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 5)]
		public string Narration { get; set; }

		
		public bool IsActive { get; set; }
	}
}