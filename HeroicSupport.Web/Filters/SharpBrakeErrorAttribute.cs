using System;
using System.Web;
using System.Web.Mvc;
using SharpBrake;

namespace HeroicSupport.Web.Filters
{
	public class SharpBrakeErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			if (filterContext.IsChildAction)
			{
				return;
			}

			// If custom errors are disabled, we need to let the normal ASP.NET exception handler
			// execute so that the user can see useful debugging information. 
			if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
			{
				return;
			}

			var exception = filterContext.Exception;

			// If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method), 
			// ignore it.
			if (new HttpException(null, exception).GetHttpCode() != 500)
			{
				return;
			}

			if (!ExceptionType.IsInstanceOfType(exception))
			{
				return;
			}

			base.OnException(filterContext);

			exception.SendToAirbrake();
		}
	}
}