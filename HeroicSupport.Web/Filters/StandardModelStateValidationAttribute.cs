using System.Web.Mvc;

namespace HeroicSupport.Web.Filters
{
	public class StandardModelStateValidationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!filterContext.Controller.ViewData.ModelState.IsValid)
			{
				filterContext.Result = new ViewResult();
			}
		}
	}
}