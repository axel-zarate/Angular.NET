using System.Web.Optimization;

namespace AxSoft.Angular.Net.Sample
{
	internal static class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery/jquery-{version}.js"));

			// Angular
			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
				"~/Scripts/angularjs/angular.js",
				"~/Scripts/angularjs/angular-route.js",
				"~/Scripts/angularjs/angular-resource.js"));

			// Bootstrap
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					"~/Scripts/bootstrap/transition.js",
					"~/Scripts/bootstrap/alert.js",
					"~/Scripts/bootstrap/modal.js",
					"~/Scripts/bootstrap/dropdown.js",
					"~/Scripts/bootstrap/button.js",
					"~/Scripts/bootstrap/collapse.js"
				));

			// Application
			bundles.Add(new ScriptBundle("~/bundles/app").Include(
				"~/Scripts/app/app.js",
				"~/Scripts/app/routes.js",
				"~/Scripts/app/controllers/*.js",
				"~/Scripts/app/directives/*.js"));

			bundles.Add(new StyleBundle("~/Content/css/app").Include(
					"~/Content/css/bootstrap.css",
					"~/Content/css/site.css"));
		}
	}
}