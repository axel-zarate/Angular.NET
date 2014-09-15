using AxSoft.Angular.Net.Sample.Models.Validations;
using AxSoft.Angular.Net.Sample.Models.Validations.Adapters;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AxSoft.Angular.Net.Sample
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode,
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			ConfigureAngularNet();
		}

		private static void ConfigureAngularNet()
		{
			AxSoft.Angular.Net.AngularConfiguration.ErrorCssClass = "has-error";
			AxSoft.Angular.Net.AngularConfiguration.HelpCssClass = "help-block";

			AxSoft.Angular.Net.Validation.AngularDataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

			// Whenever a PostalCodeAttribute attribute is found, use the included RegularExpressionAttributeAdapter
			AxSoft.Angular.Net.Validation.AngularDataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(PostalCodeAttribute),
				typeof(AxSoft.Angular.Net.Validation.RegularExpressionAttributeAdapter));

			// Whenever a DoubleAttribute attribute is found, use the custom DoubleAttributeAdapter
			AxSoft.Angular.Net.Validation.AngularDataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DoubleAttribute), typeof(DoubleAttributeAdapter));
		}
	}
}