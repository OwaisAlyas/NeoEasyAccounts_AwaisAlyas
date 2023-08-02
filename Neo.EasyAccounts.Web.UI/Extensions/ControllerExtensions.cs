using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI
{
	public static class ControllerExtensions
	{
		public static string GetControllerName(this Controller controller)
		{
			string controllerName = controller.RouteData.Values["controller"].ToString();
			return controllerName;
		}
		public static string GetActionName(this Controller controller)
		{
			string actionName = controller.RouteData.Values["action"].ToString();
			return actionName;
		}
		public static string GetControllerAndActionName(this Controller controller)
		{
			string controllerAndActionName = string.Format("{0} - {1}", controller.RouteData.Values["controller"], controller.RouteData.Values["action"]);
			return controllerAndActionName;
		}

		public static void AddErrorToViewBag(this Controller controller, List<ViewModels.ActionOutput> actionMessges)
		{
			var m = controller.ViewBag.ActionOutput as List<ViewModels.ActionOutput>;
			if (m == null) m = new List<ViewModels.ActionOutput>();

			m.AddRange(actionMessges);

			controller.ViewBag.ActionOutput = m;
		}
	}
}