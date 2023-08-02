using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI.Areas.Locations.ViewModels
{
	public class AreaViewModel
	{
		public long ID { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "Country")]
		public long CountryID { get; set; }
		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "State")]
		public long StateID { get; set; }
		[Required(ErrorMessage = "{0} Required")]
		[Display(Name = "City")]
		public long CityID { get; set; }


		[Required(ErrorMessage = "{0} Required")]
		[StringLength(250, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 3)]
		public string Name { get; set; }

		[Required(ErrorMessage = "{0} Required")]
		[StringLength(250, ErrorMessage = "The {0} must be between {2} & {1} characters long.", MinimumLength = 3)]
		public string Code { get; set; }

		public string Description { get; set; }

		public SelectList Countries { get; set; }
		public SelectList States { get; set; }
		public SelectList Cities { get; set; }

		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateModified { get; set; }

		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
	}
}