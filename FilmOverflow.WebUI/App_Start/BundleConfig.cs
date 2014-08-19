using System.Web.Optimization;

namespace FilmOverflow.WebUI
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
			"~/Scripts/jquery.unobtrusive-ajax.js"));
			
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));
			
			bundles.Add(new ScriptBundle("~/bundles/jqueryfileupload").Include(
						"~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
						"~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js",
						"~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
						"~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/jqueryfileupload-css").Include(
					  "~/Content/jQuery.FileUpload/css/jquery.fileupload.css"));

			bundles.Add(new StyleBundle("~/Content/font-css").Include(
					  "~/Content/font-awesome.css"));

			// Set EnableOptimizations to false for debugging. For more information,
			// visit http://go.microsoft.com/fwlink/?LinkId=301862
			//BundleTable.EnableOptimizations = true;
		}
	}
}
