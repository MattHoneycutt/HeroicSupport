using System.Web.Mvc;
using System.Web.Security;

namespace HeroicSupport.Web.ActionResults
{
	public class LogoutResult : ActionResult
	{
		public override void ExecuteResult(ControllerContext context)
		{
			FormsAuthentication.SignOut();
			var redirectResult = new RedirectResult("~/");
			redirectResult.ExecuteResult(context);
		}
	}
}