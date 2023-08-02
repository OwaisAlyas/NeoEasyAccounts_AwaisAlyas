using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Logging
{
	public enum LoggerNames
	{
		[Description("Neo.EasyAccounts.Web.UI")]
		Web = 0,
		[Description("Neo.EasyAccounts.Data")]
		Data = 1,
		[Description("Neo.EasyAccounts.Services")]
		Services = 2,
		[Description("Neo.EasyAccounts.Models")]
		Models = 3,
		[Description("Neo.Auth")]
		Auth = 4,
	}
}
