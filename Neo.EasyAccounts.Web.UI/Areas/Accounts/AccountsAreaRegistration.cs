﻿using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI.Areas.Accounts
{
	public class AccountsAreaRegistration : AreaRegistration 
	{
		public override string AreaName 
		{
			get 
			{
				return "Accounts";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) 
		{
			context.MapRoute(
				"Accounts_default",
				"Accounts/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}