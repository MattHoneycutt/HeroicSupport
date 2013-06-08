using System.Linq;
using OpenQA.Selenium;
using SpecsFor.Mvc;

namespace HeroicSupport.IntegrationTests.Web
{
	public static class TestHelpers
	{
		public static string SuccessMessage(this MvcWebApp app)
		{
			var element = app.Browser.FindElements(By.ClassName("alert-success")).FirstOrDefault();
			return element == null ? null : element.Text;
		}
	}
}