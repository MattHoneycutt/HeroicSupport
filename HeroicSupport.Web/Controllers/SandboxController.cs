using System;
using System.Web.Mvc;
using SharpBrake;

namespace HeroicSupport.Web.Controllers
{
	public class SandboxController : Controller
	{
		public ActionResult Error()
		{
			throw new InvalidOperationException("Test: unhandled exception from Sandbox!");
		}

		public ActionResult SharpBrake()
		{
			var exception = new InvalidOperationException("Error sent directly to SharpBrake...");
			exception.SendToAirbrake();

			return Content("Exception sent?");
		}
	}
}