using System.Web.Mvc;

namespace AxSoft.Angular.Net.Sample.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		// This will handle all AJAX requests for Angular views
		protected override void HandleUnknownAction(string actionName)
		{
			var action = RouteData.GetRequiredString("action");

			if (!string.IsNullOrEmpty(action))
			{
				var result = ViewEngines.Engines.FindPartialView(ControllerContext, action);
				if (result != null && result.View != null && result.ViewEngine != null)
				{
					var viewContext = new ViewContext(ControllerContext, result.View, ViewData, TempData, Response.Output);
					result.View.Render(viewContext, Response.Output);
					result.ViewEngine.ReleaseView(ControllerContext, result.View);
					return;
				}
			}

			base.HandleUnknownAction(actionName);
		}
	}
}