using Neo.EasyAccounts.Web.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.ViewModels
{
	public class ActionOutput
	{
		public ActionOutput()
		{
			AlertTimeout = 3000;
		}

		public string MessageTitle { get; set; }
		public string MessageDetails { get; set; }

		public AlertTypes AlertType { get; set; }

		public AlertActionTypes ActionType { get; set; }

		public Exception Exception { get; set; }

		public int AlertTimeout { get; set; }
	}

	public abstract class ViewModelsBase
	{
		public ActionOutput ActionOutput { get; set; }
	}
}