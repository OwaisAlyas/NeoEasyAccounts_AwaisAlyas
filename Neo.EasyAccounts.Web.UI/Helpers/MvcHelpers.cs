using Neo.EasyAccounts.Web.UI.Helpers;
using Neo.EasyAccounts.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Neo.EasyAccounts.Web;

namespace Neo.EasyAccounts.Web.UI
{
	public static class MvcHelpers
	{
		public static MvcHtmlString URLPartialView(this HtmlHelper htmlHelper, string url)
		{
			var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

			return MvcHtmlString.Create(urlHelper.Content(url));
		}

		public static MvcHtmlString MailTo(this HtmlHelper helper, string emailAddress, string displayText = null)
		{
			if (string.IsNullOrEmpty(displayText))
			{
				displayText = emailAddress;
			}
			var sb = string.Format("<a href=\"{0}{1}\" title=\"{1}\">{2}</a>",
			CharEncode("mailto:"), CharEncode(emailAddress), CharEncode(displayText));
			return new MvcHtmlString(sb);
		}
		private static string CharEncode(string value)
		{
			var enc = Encoding.Default;
			var retval = "";
			for (var i = 0; i < value.Length; i++)
			{
				retval += "&#" + enc.GetBytes(new[] { Convert.ToChar(value.Substring(i, 1)) })[0] + ";";
			}
			return retval;
		}

		//public static MvcHtmlString TypeaheadFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<string> source, int items = 8)
		//{
		//	if (expression == null) throw new ArgumentNullException("expression");

		//	if (source == null) throw new ArgumentNullException("source");

		//	var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(source);

		//	return htmlHelper.TextBoxFor(
		//		expression,
		//		new
		//		{
		//			autocomplete = "off",
		//			data_provide = "typeahead",
		//			data_items = items,
		//			data_source = jsonString
		//		}
		//	);
		//}

		private static string getCssClassFromAlertType(AlertTypes alertType)
		{
			string alertCSSClass = null;
			switch (alertType)
			{
				case AlertTypes.Info:
					alertCSSClass = "alert-info";
					break;
				case AlertTypes.Success:
					alertCSSClass = "alert-success";
					break;
				case AlertTypes.Warning:
					alertCSSClass = "alert-warning";
					break;
				case AlertTypes.Error:
					alertCSSClass = "alert-danger";
					break;
				default:
					alertCSSClass = "";
					break;
			}
			return alertCSSClass;
		}
		private static string getAlertIconFromAlertType(AlertTypes alertType)
		{
			string alertIcon = null;
			switch (alertType)
			{
				case AlertTypes.Info:
					alertIcon = "glyphicon glyphicon-info-sign";
					break;
				case AlertTypes.Success:
					alertIcon = "glyphicon glyphicon-ok-sign";
					break;
				case AlertTypes.Warning:
					alertIcon = "glyphicon glyphicon-warning-sign";
					break;
				case AlertTypes.Error:
					alertIcon = "glyphicon glyphicon-remove-sign";
					break;
				default:
					alertIcon = "";
					break;
			}
			return alertIcon;
		}
		private static string getMessageMarkupFromActionOutput(ActionOutput actionOuputInfo)
		{
			string outputFormat =
@"<div class='alert {0} alert-dismissible fade in' role='alert' id='alert_placeholderDynamic'>
	<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
	<span class='{1}'></span>  {2}: {3}
	<script> setTimeout(function () {{$('#alert_placeholderDynamic').slideUp();}}, {4});</script>
</div>
"
			, outputWithExceptionFormat =
@"<div class='alert {0} alert-dismissible fade in' role='alert' id='alert_placeholderDynamic'>
	<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
	<span class='{1}'></span>  {2}: {4} <div><strong>Details</strong> : {5}</div>
	<script> setTimeout(function () {{$('#alert_placeholderDynamic').slideUp();}}, {3});</script>
</div>
"
			, alertCSSClass = getCssClassFromAlertType(actionOuputInfo.AlertType),
				alertIcon = getAlertIconFromAlertType(actionOuputInfo.AlertType),
				exceptionDetailedMessage = actionOuputInfo.Exception.Translate(),
				alertHtml = string.Empty;

			if (actionOuputInfo.Exception == null)
			{
				alertHtml = string.Format(outputFormat, alertCSSClass, alertIcon, actionOuputInfo.MessageTitle, actionOuputInfo.MessageDetails, actionOuputInfo.AlertTimeout);
			}
			else if (actionOuputInfo.Exception != null && actionOuputInfo.MessageTitle.HasValue() && actionOuputInfo.MessageDetails.HasValue())
			{
				alertHtml = string.Format(outputWithExceptionFormat, 
					alertCSSClass, alertIcon, actionOuputInfo.MessageTitle, actionOuputInfo.AlertTimeout, actionOuputInfo.MessageDetails, exceptionDetailedMessage);
			}
			else if (actionOuputInfo.Exception != null && (!actionOuputInfo.MessageTitle.HasValue() || !actionOuputInfo.MessageDetails.HasValue()))
			{
				outputWithExceptionFormat = outputWithExceptionFormat.Replace(": {4} <div><strong>Details</strong> : {5}</div>", "");

				alertHtml = string.Format(outputWithExceptionFormat, alertCSSClass, alertIcon, exceptionDetailedMessage, actionOuputInfo.AlertTimeout);
			}

			return alertHtml;
		}

		/// <summary>
		/// Displays multiple alert messages
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		public static MvcHtmlString ShowMessages(this HtmlHelper helper)
		{
			var actionOuputs = helper.ViewContext.ViewBag.ActionOutput as List<ViewModels.ActionOutput>;

			if (actionOuputs == null) return new MvcHtmlString("");
			string alertHtml = string.Empty;

			foreach (var item in actionOuputs)
			{
				alertHtml += getMessageMarkupFromActionOutput(item);
			}

			return new MvcHtmlString(alertHtml);
		}

		//public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
		//{
		//	return new Pager(htmlHelper, pageSize, currentPage, totalItemCount);
		//}
		//public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
		//{
		//	return new Pager(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
		//}
		//public static Pager<TModel> Pager<TModel>(this HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount)
		//{
		//	return new Pager<TModel>(htmlHelper, pageSize, currentPage, totalItemCount);
		//}
		//public static Pager<TModel> Pager<TModel>(this HtmlHelper<TModel> htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
		//{
		//	return new Pager<TModel>(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
		//}

		public static string Id(this HtmlHelper htmlHelper)
		{
			var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

			if (routeValues.ContainsKey("id"))
				return (string)routeValues["id"];
			else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
				return HttpContext.Current.Request.QueryString["id"];

			return string.Empty;
		}

		public static string Controller(this HtmlHelper htmlHelper)
		{
			var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

			if (routeValues.ContainsKey("controller"))
				return (string)routeValues["controller"];

			return string.Empty;
		}

		public static string Action(this HtmlHelper htmlHelper)
		{
			var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

			if (routeValues.ContainsKey("action"))
				return (string)routeValues["action"];

			return string.Empty;
		}
	}
}