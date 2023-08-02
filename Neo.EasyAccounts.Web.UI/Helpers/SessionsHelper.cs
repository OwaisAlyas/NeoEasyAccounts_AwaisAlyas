using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	/// <summary>
	/// Containing all the sessions as public properties
	/// </summary>
	public class SessionsHelper
	{
		public static string ErrorMessage
		{
			get { return Convert.ToString(HttpContext.Current.Session["Name"]); }
			set { HttpContext.Current.Session["Name"] = value; }
		}
	}
}