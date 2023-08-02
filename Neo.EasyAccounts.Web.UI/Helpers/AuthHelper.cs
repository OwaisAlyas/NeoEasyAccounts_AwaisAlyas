using ClassifiedAds.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI
{
	public class AuthHelper
	{
		public static string GetUserByID(long ID)
		{
			string username = null;
			using (IdentityContext context = new IdentityContext())
			{
				var user = context.Users.Find(ID);
				if (user != null)
				{
					username = user.UserName;
				}
			}
			return username;
		}
		public static string GetUserByID(long? ID)
		{
			string username = null;

			if (ID != null && ID.HasValue)
			{
				username = GetUserByID(ID.Value);
			}
			return username;
		}
	}
}