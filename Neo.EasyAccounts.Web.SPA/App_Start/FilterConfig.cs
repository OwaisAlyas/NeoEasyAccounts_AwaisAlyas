﻿using System.Web;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.SPA
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
