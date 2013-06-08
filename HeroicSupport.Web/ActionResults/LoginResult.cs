using System.Web.Mvc;
using System.Web.Security;

namespace HeroicSupport.Web.ActionResults
{
	public class LoginResult : ActionResult
	{
		public string UserName { get; set; }

		public LoginResult(string userName)
		{
			UserName = userName;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			FormsAuthentication.SetAuthCookie(UserName, true);
			var redirectResult = new RedirectResult("~/");
			redirectResult.ExecuteResult(context);
		}
	}
}