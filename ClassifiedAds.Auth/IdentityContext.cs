using ClassifiedAds.Model.Admin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using Neo.Auth.ViewModels;

namespace ClassifiedAds.Auth
{
	public class IdentityContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
	{
		public IdentityContext()
			: base("MSSQLConnection")
		{
			//Database.SetInitializer<IdentityContext>(null);// Remove default initializer
			//Configuration.ProxyCreationEnabled = false;
			//Configuration.LazyLoadingEnabled = false;
		}

		//Identity and Authorization
		public DbSet<UserLogin> UserLogins { get; set; }
		public DbSet<UserClaim> UserClaims { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<ApplicationClaim> Claims { get; set; }

		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			//modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Entity<User>().Ignore(t => t.Age).ToTable("Users");
			modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasMaxLength(500);
			modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(50);
			modelBuilder.Entity<User>().Property(u => u.FirstName).HasMaxLength(250);
			modelBuilder.Entity<User>().Property(u => u.LastName).HasMaxLength(250);

			modelBuilder.Entity<Role>().ToTable("Roles");
			modelBuilder.Entity<UserRole>().ToTable("UserRoles");
			modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
			modelBuilder.Entity<UserClaim>().ToTable("UserClaims");

			modelBuilder.Entity<ApplicationClaim>().ToTable("Claims");
			modelBuilder.Entity<ApplicationClaim>().Property(u => u.Name).HasMaxLength(250).IsRequired();
			modelBuilder.Entity<ApplicationClaim>().Property(u => u.Description).HasMaxLength(500);

			modelBuilder.Entity<UserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
			modelBuilder.Entity<UserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
		}

		//static IdentityContext()
		//{
		//	Database.SetInitializer<IdentityContext>(new InitializerDropCreateDatabaseAlways());
		//}

		public bool RoleExists(IdentityRoleManager roleManager, string name)
		{
			return roleManager.RoleExists(name);
		}

		public IdentityResult CreateRole(IdentityRoleManager _roleManager, string name, string description = "")
		{
			var idResult = _roleManager.Create<Role, long>(new Role(name, description));
			return idResult;
		}

		public IdentityResult AddClaimToUser(IdentityUserManager _userManager, long userId, string claim, string value)
		{
			var userClaim = new Claim(claim, value);

			var idResult = _userManager.AddClaim(userId, userClaim);
			return idResult;
		}

		public IdentityResult UpdateRole(IdentityRoleManager _roleManager, RoleViewModel model)
		{
			var role = _roleManager.FindById(model.ID);
			
			if (role == null) role = new Role();

			role.Name = model.RoleName;
			role.Description = model.Description;
			var idResult = _roleManager.Update<Role, long>(role);
			return idResult; 
		}

		public bool AddUserToRole(IdentityUserManager _userManager, long userId, string roleName)
		{
			var idResult = _userManager.AddToRole(userId, roleName);
			return idResult.Succeeded;
		}

		public void ClearUserRoles(IdentityUserManager userManager, long userId)
		{
			var user = userManager.FindById(userId);
			var currentRoles = new List<Role>();

			currentRoles.AddRange(user.UserRoles);

			foreach (var role in currentRoles)
			{
				userManager.RemoveFromRole(userId, role.Name);
			}
		}

		public void RemoveFromRole(IdentityUserManager userManager, long userId, string roleName)
		{
			userManager.RemoveFromRole(userId, roleName);
		}

		public void DeleteRole(IdentityContext context, IdentityUserManager userManager, long roleId)
		{
			var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.Id == roleId));
			var role = context.Roles.Find(roleId);

			foreach (var user in roleUsers)
			{
				this.RemoveFromRole(userManager, user.Id, role.Name);
			}
			context.Roles.Remove(role);
			context.SaveChanges();
		}

		public static IdentityContext Create()
		{
			return new IdentityContext();
		}
		
		public void Seed(IdentityContext context)
		{
			var claimsList = new List<ApplicationClaim>() { 
				new ApplicationClaim(){ Name = "CanApprove", Description = "Authorized to Approve things in the system"},
				new ApplicationClaim(){ Name = "CanReject", Description = "Authorized to Reject things in the system"},
				new ApplicationClaim(){ Name = "CanApproveVoucher", Description = "Authorized to Approve Voucher in the system"},
			};
			context.Claims.AddRange(claimsList);
			context.SaveChanges();

			if (!context.Roles.Any(r => r.Name == "SysAdmin" && r.Name == "SysUser"))
			{
				var store = new RoleStore<Role, long, UserRole>(context);
				var manager = new RoleManager<Role, long>(store);

				var role = new Role();
				role.Name = "SysAdmin";
				role.Description = "System Administrator for Global Access";

				manager.Create(role);
				role = new Role();
				role.Name = "SysUser";
				role.Description = "System User for Restricted Access to Business Domain Activity";

				manager.Create(role);
			}

			if (!context.Users.Any(u => u.UserName == "administrator"))
			{
				var store = new UserStore<User, Role, long, UserLogin, UserRole, UserClaim>(context);
				var manager = new UserManager<User, long>(store);
				var user = new User { UserName = "administrator" };

				manager.Create(user, "administrator");
				manager.AddToRole(user.Id, "SysAdmin");

				var canApproveClaim = context.Claims.FirstOrDefault(d => d.Name.Equals("CanApprove"));
				var canRejectClaim = context.Claims.FirstOrDefault(d => d.Name.Equals("CanReject"));

				var userClaim = new Claim(canApproveClaim.Name, "true");

				var result = manager.AddClaim(user.Id, userClaim);
				
				userClaim = new Claim(canRejectClaim.Name, "true");
				result = manager.AddClaim(user.Id, userClaim);
			};
		}
		
		public class InitializerCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<IdentityContext>
		{
			protected override void Seed(IdentityContext context)
			{
				context.Seed(context);

				base.Seed(context);
			}
		}
		public class InitializerDropCreateDatabaseAlways : DropCreateDatabaseAlways<IdentityContext>
		{
			protected override void Seed(IdentityContext context)
			{
				context.Seed(context);

				base.Seed(context);
			}
		}
		public class InitializerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<IdentityContext>
		{
			protected override void Seed(IdentityContext context)
			{
				context.Seed(context);

				base.Seed(context);
			}
		}

		public System.Data.Entity.DbSet<Neo.Auth.ViewModels.ClaimViewModel> ClaimViewModels { get; set; }
	}	
}
