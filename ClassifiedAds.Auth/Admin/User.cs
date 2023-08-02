using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Neo.Auth.ViewModels;

namespace ClassifiedAds.Model.Admin
{
	public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, IUser<long>
	{
		public User()
		{
			
		}

		public User(UserViewModel model)
		{
			this.DateOfBirth = model.DateOfBirth;
			this.Email = model.Email;
			this.UserName = this.Email;
			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.PhoneNumber = model.PhoneNumber;
		}
		
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, long> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public long? CityID { get; set; }
		public long? StateID { get; set; }
		public DateTime? DateOfBirth { get; set; }


		public int Age
		{
			get
			{
				var today = DateTime.Today;
				int age = today.Year - (DateOfBirth != null && DateOfBirth.HasValue ? DateOfBirth.Value.Year : 0);

				if (DateOfBirth > today.AddYears(-age))
					age--;

				return age;
			}
		}

		public ICollection<Role> UserRoles { get; set; }
	}
}
