using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Neo.EasyAccounts.Web
{
	public static class ExceptionExtensions
	{
		private static string GetSpaces(int numberOfSpaces, bool isHtml = true)
		{
			string spaces = "";
			while (numberOfSpaces != 0)
			{
				spaces += isHtml ? "&nbsp;&nbsp;&nbsp;&nbsp;" : "\t";
				numberOfSpaces--;
			}
			return spaces;
		}

		public static string Translate(this Exception ex, bool newLineHTML = false)
		{
			if (ex == null) return string.Empty;
			var errorMessageBuilder = new StringBuilder();
			int i = 0;
			do
			{
				errorMessageBuilder.Append(string.Format(newLineHTML ? "{0}&nbsp;-&nbsp;{1} {2}<br />" : "{0} - {1}: {2}\n", GetSpaces(i, newLineHTML), ex.GetType().Name, ex.Message + ex.StackTrace));
				ex = ex.InnerException;
				i++;
			} while (ex != null);

			errorMessageBuilder = newLineHTML ? errorMessageBuilder.Remove(0, 13).Remove(errorMessageBuilder.Length - 5, 5) : errorMessageBuilder.Remove(0, 3);

			string errorMessage = errorMessageBuilder.ToString();
			return errorMessage;
		}
	}
}