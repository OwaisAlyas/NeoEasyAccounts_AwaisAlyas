using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	public class ReflectionsHelper
	{
		public static string WhoseThere([CallerMemberName] string memberName = "")
		{
			return memberName;
		}
	}
}