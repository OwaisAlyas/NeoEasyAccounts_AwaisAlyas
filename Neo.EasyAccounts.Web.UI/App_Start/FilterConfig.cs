﻿using System.Web;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
