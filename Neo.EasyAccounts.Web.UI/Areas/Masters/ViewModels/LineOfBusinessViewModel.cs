
namespace Neo.EasyAccounts.Web.UI.Areas.Masters.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class LineOfBusinessViewModel
	{
		public long ID { get; set; }

		[Display(Name = "Manager Name")]
		[Required(ErrorMessage = "{0} Required")]
		public string ManagerName { get; set; }

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