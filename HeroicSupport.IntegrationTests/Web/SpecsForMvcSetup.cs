using HeroicSupport.Web.App_Start;
using NUnit.Framework;
using SpecsFor.Mvc;

namespace HeroicSupport.IntegrationTests.Web
{
	[SetUpFixture]
	public class SpecsForMvcSetup 
	{
		private SpecsForIntegrationHost _host;

		[SetUp]
		public void Startup()
		{
			var config = new SpecsForMvcConfig();
			config.UseIISExpress().With(Project.Named("HeroicSupport.Web")).ApplyWebConfigTransformForConfig("Test");
			config.BuildRoutesUsing(RouteConfig.RegisterRoutes);
			config.UseBrowser(BrowserDriver.Chrome);
			config.AuthenticateBeforeEachTestUsing<RegularUserAuthenticator>();

			config.InterceptEmailMessagesOnPort(12345);
			
			config.Use<SeedDataConfig>();

			_host = new SpecsForIntegrationHost(config);
			_host.Start();
		}

		[TearDown]
		public void Shutdown()
		{
			_host.Shutdown();
		}
	}
}