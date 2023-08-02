using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	public enum AlertTypes : int
	{
		[Description("none")]
		None = 0,
		[Description("info")]
		Info = 1,
		[Description("success")]
		Success = 2,
		[Description("warning")]
		Warning = 3,
		[Description("error")]
		Error = 4
	}
}