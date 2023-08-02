using Neo.Auth;
using Neo.EasyAccounts.Service;
using Neo.EasyAccounts.Web.UI.Mappings;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Neo.EasyAccounts.Web.UI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			InitializerServiceFactory.DropCreateDatabaseIfModelChanges();
			DataInitializerFactory.DropCreateDatabaseIfModelChanges();

			AutoFacBootsrtapper.Run();
			AutoMapperConfiguration.Configure();
		}
	}
}
