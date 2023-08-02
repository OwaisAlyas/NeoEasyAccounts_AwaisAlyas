using ClassifiedAds.Model.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedAds.Auth
{
	public class IdentityUserStore : UserStore<User, Role, long, UserLogin, UserRole, UserClaim>, IUserStore<User, long>, IDisposable
	{
		public IdentityUserStore(IdentityContext context)
			: base(context)
		{
		}

		public IdentityUserStore()
			: this(new IdentityContext())
		{
			base.DisposeContext = true;
		}
	}

	public class IdentityRoleStore : RoleStore<Role, long, UserRole>, IQueryableRoleStore<Role, long>, IRoleStore<Role, long>, IDisposable
	{
		public IdentityRoleStore()
			: base(new IdentityDbContext())
		{
			base.DisposeContext = true;
		}

		public IdentityRoleStore(DbContext context)
			: base(context)
		{
		}
	}
}
