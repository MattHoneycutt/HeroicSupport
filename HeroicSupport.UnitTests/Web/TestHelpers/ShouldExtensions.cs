using System.Web.Mvc;
using HeroicSupport.Web.Helpers;
using Should;

namespace HeroicSupport.UnitTests.Web.TestHelpers
{
	public static class ShouldExtensions
	{
		public static void ShouldHaveSuccessMessage<TController>(this TController controller) where TController : Controller
		{
			controller.TempData.Keys.ShouldContain(Alerts.SUCCESS);
		}

		public static void ShouldHaveErrorMessage<TController>(this TController controller) where TController : Controller
		{
			controller.TempData.Keys.ShouldContain(Alerts.ERROR);
		}
	}
}