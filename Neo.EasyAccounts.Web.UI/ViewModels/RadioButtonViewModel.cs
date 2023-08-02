using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.ViewModels
{
	public class RadioButtonViewModel
	{
		public List<RadioButtonItem> RadioButtonList { get; set; }
		public string SelectedRadioButton { get; set; }
	}
	public class RadioButtonItem
	{
		public string Name { get; set; }
		public bool Selected { get; set; }
	}
}