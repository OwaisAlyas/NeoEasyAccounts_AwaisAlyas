using System.Web;
using System.Web.Optimization;

namespace Neo.EasyAccounts.Web.UI
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*",
						"~/Scripts/JqueryValidateHelper.js"
						));

			bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive-ajax").Include(
				 "~/Scripts/jquery.unobtrusive-ajax.min.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));
			bundles.Add(new ScriptBundle("~/bundles/AdminLTE").Include(
					  "~/Scripts/jquery.slimscroll.js",
					  "~/Scripts/fastclick.js",
					  "~/Scripts/app.js"
					  ));

			bundles.Add(new ScriptBundle("~/Scripts/DataTables").Include(
				"~/Scripts/DataTables/jquery.dataTables.js",
				"~/Scripts/DataTables/dataTables.bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/iCheck").Include(
					  "~/Scripts/jquery.icheck.js"));
			bundles.Add(new ScriptBundle("~/bundles/Select2").Include(
					  "~/Scripts/select2.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/font-awesome.css"));

			bundles.Add(new StyleBundle("~/Content/AdminLTE").Include(
					  "~/Content/AdminLTE.css"));

			bundles.Add(new StyleBundle("~/Content/AdminLTE-Blue").Include(
					  "~/Content/skins/skin-blue.css"));

			bundles.Add(new StyleBundle("~/Content/DataTables/css").Include(
					  "~/Content/DataTables/css/dataTables.bootstrap.css"));

			bundles.Add(new StyleBundle("~/Content/Select2").Include(
					  "~/Content/css/select2.css"));


			bundles.Add(new StyleBundle("~/Content/iCheck-minimal").Include(
					  "~/Content/iCheck/minimal/minimal.css"));
			bundles.Add(new StyleBundle("~/Content/iCheck-minimal-blue").Include(
					  "~/Content/iCheck/minimal/blue.css"));

			// Set EnableOptimizations to false for debugging. For more information,
			// visit http://go.microsoft.com/fwlink/?LinkId=301862
			BundleTable.EnableOptimizations = !HttpContext.Current.IsDebuggingEnabled;
		}
	}
}
