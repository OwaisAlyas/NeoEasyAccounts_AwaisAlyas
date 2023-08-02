using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Neo.EasyAccounts.Web.UI
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			
			//Areas.Accounts.AccountsAreaRegistration.RegisterAllAreas();
			//Areas.Locations.LocationsAreaRegistration.RegisterAllAreas();
			//Areas.Masters.MastersAreaRegistration.RegisterAllAreas();
			//Areas.Vouchers.VouchersAreaRegistration.RegisterAllAreas();

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
