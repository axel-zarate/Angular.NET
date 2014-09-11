using System.Web;
using System.Web.Mvc;

namespace AxSoft.Angular.Net.Sample
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}