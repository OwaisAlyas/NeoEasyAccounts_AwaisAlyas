using ClassifiedAds.Model.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedAds.Auth
{
	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	public class IdentityUserManager : UserManager<User, long>
	{
		public IdentityUserManager(IUserStore<User, long> store)
			: base(store)
		{
		}

		public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager> options, IOwinContext context)
		{
			var manager = new IdentityUserManager(new UserStore<User, Role, long, UserLogin, UserRole, UserClaim>(context.Get<IdentityContext>()));
			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<User, long>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			// Configure user lockout defaults
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug in here.
			manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, long>
			{
				MessageFormat = "Your security code is: {0}"
			});
			manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, long>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is: {0}"
			});
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}
		
	public class IdentityRoleManager : RoleManager<Role, long>
	{
		IdentityRoleManager _roleManager;
		public IdentityRoleManager(IRoleStore<Role, long> roleStore)
			: base(roleStore)
		{

		}

		public static IdentityRoleManager Create(IdentityFactoryOptions<IdentityRoleManager> options, IOwinContext context)
		{
			return new IdentityRoleManager(new RoleStore<Role, long, UserRole>(context.Get<IdentityContext>()));
		}

		private void SetManager(IdentityContext _db)
		{
			_roleManager = new IdentityRoleManager(new RoleStore<Role, long, UserRole>(_db));
		}
	}

	//Configure the application sign-in manager which is used in this application.
	public class IdentitySignInManager : SignInManager<User, long>
	{
		public IdentitySignInManager(IdentityUserManager userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{
		}

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
		{
			return user.GenerateUserIdentityAsync((IdentityUserManager)UserManager);
		}

		public static IdentitySignInManager Create(IdentityFactoryOptions<IdentitySignInManager> options, IOwinContext context)
		{
			return new IdentitySignInManager(context.GetUserManager<IdentityUserManager>(), context.Authentication);
		}
	}
}
