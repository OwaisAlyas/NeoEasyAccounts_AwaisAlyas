using Neo.EasyAccounts.Service.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neo.EasyAccounts;

namespace ClassifiedAds.Web.AdminPanel.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly Neo.Logging.ILogger logger;
		
		public HomeController(ICityService service)
		{
			logger = Neo.Logging.LoggerFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
		
		}

		public ActionResult Index()
		{
			try
			{
				//logger.Info("Testing the Log4net logging. . .");
				//throw new Exception("Elmah testing. . . ");
			}
			catch (Exception ex)
			{
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
			}
			return View();
		}
	}
}