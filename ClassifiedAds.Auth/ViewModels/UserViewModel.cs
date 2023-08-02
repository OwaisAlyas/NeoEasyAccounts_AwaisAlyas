using ClassifiedAds.Model.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Auth.ViewModels
{
	public class UserViewModel
	{
		public UserViewModel()
		{

		}
		public UserViewModel(User model)
		{
			if (model == null) return;
			this.ID = model.Id;
			this.Email = model.Email;
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.PhoneNumber = model.PhoneNumber;
			this.DateOfBirth = model.DateOfBirth;
		}

		[Key]
		public long ID { get; set; }
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[Display(Name = "Date of Birth")]
		public DateTime? DateOfBirth { get; set; }

		public string FullName { get { return FirstName + " " + LastName; } }

		public string PhoneNumber { get; set; }
		public string Roles { get; set; }

		public List<ClaimViewModel> Claims { get; set; }

		public Dictionary<string, string> RolesList { get; set; }
	}
}
