using System.Web.Mvc;
using HeroicSupport.Web.Filters;

namespace HeroicSupport.Web.App_Start
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new SharpBrakeErrorAttribute());
			//By default, everything is locked down.  Controllers have to opt-in to be anonymous. 
			filters.Add(new AuthorizeAttribute());
		}
	}
}