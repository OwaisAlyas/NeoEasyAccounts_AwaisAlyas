﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.ViewModels
{
	//public class Select2ViewModel
	//{
	//}
	public class Select2PagedResult
	{
		public int Total { get; set; }
		public List<Select2Result> Results { get; set; }
	}
	public class Select2Result
	{
		public string id { get; set; }
		public string text { get; set; }
	} 
}