using System.Web.Mvc;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using HeroicSupport.Web.Filters;
using HeroicSupport.Web.Helpers;
using HeroicSupport.Web.Models.RegisterNewUser;
using Microsoft.Web.Mvc;

namespace HeroicSupport.Web.Controllers
{
	[AllowAnonymous]
    public class RegisterNewUserController : Controller
    {
		private readonly IRepository<User> _users;

		public RegisterNewUserController(IRepository<User> users)
		{
			_users = users;
		}

		public ActionResult Register()
	    {
		    return View();
	    }

		[HttpPost]
		[StandardModelStateValidation]
		public ActionResult Register(RegisterForm form)
		{
			var user = Core.Domain.User.CreateNewUser(form.DisplayName, form.EmailAddress, form.Password);

			_users.Add(user);
			_users.Save();

			this.Success("You are now registered!  Please log in to continue...");

			return this.RedirectToAction<AuthController>(c => c.Login());
		}

    }
}
