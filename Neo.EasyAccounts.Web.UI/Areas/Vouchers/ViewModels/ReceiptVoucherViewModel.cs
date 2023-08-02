
namespace Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ReceiptVoucherViewModel
	{
		public ReceiptVoucherViewModel()
		{
			Date = DateTime.Now;
		}
		public long ID { get; set; }

		public string Type { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		public DateTime Date { get; set; }

		//[Required(ErrorMessage = "{0} Required")]
		[StringLength(250, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 3)]
		[Display(Name = "Voucher No")]
		public string Number { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[Display(Name="Customer")]
		public long CustomerID { get; set; }

		public string Description { get; set; }

		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateModified { get; set; }

		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }

		[RegularExpression("(^[0-9]*[02468]$)", ErrorMessage = "Count must be a positive even number")]
		[Display(Name = "Details RowCount")]
		[Range(2, 100, ErrorMessage = "{0} Must be in between {1} and {2}")]
		public int RowCount { get; set; }

		public SelectList Cutomers { get; set; }
		public SelectList Accounts { get; set; }
		public IEnumerable<PaymentVoucherDetailViewModel> DetailsList { get; set; }
	}
}