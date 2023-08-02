using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Areas.Vouchers.ViewModels
{
	public class KODetailsViewModel<T> where T : class
	{
		public long VoucherID { get; set; }
		public List<T> Details { get; set; }
	}
}