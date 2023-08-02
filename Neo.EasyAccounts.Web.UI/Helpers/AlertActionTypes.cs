using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	public enum AlertActionTypes : int
	{
		[Description("Added")]
		Added = 1,
		[Description("Updated")]
		Updated = 2,
		[Description("Saved")]
		Saved = 2,
		[Description("Deleted")]
		Deleted = 3,
		[Description("Read")]
		Read = 4
	}
}