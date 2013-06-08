using System;
using System.Linq;
using System.Web.Mvc;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using HeroicSupport.Web.ActionResults;
using HeroicSupport.Web.Filters;
using HeroicSupport.Web.Models.Auth;
using HeroicSupport.Web.Helpers;
using Microsoft.Web.Mvc;

namespace HeroicSupport.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IRepository<User> _users;

		public AuthController(IRepository<User> users)
		{
			_users = users;
		}

		[AllowAnonymous, OutputCache(Duration = 60)]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost, StandardModelStateValidation, AllowAnonymous]
		public ActionResult Login(LoginForm form)
		{
			var user = _users.Query().FirstOrDefault(u => u.EmailAddress == form.EmailAddress);

			if (user == null || !user.IsThisTheUsersPassword(form.Password))
			{
				ModelState.AddModelError("", "The username or password is invalid.");
				return View();
			}

			return new LoginResult(user.EmailAddress);
		}

		public ActionResult Logout()
		{
			return new LogoutResult();
		}

		public ActionResult ChangePassword()
		{
			return View();
		}


		[HttpPost, StandardModelStateValidation]
		public ActionResult ChangePassword(ChangePasswordForm form)
		{
			var user = _users.Query().SingleOrDefault(u => u.EmailAddress == User.Identity.Name);

			if (user == null)
			{
				throw new InvalidOperationException("Unable to find user...");
			}

			if (!user.IsThisTheUsersPassword(form.CurrentPassword))
			{
				ModelState.AddModelError("", "The current password is incorrect.");
				return View();
			}

			user.SetPassword(form.NewPassword);
			_users.Save();

			this.Success("Your password has been changed.");

			return this.RedirectToAction<DashboardController>(c => c.Index());
		}
	}
}