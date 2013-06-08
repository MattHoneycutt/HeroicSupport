using System.Web.Routing;
using HeroicSupport.Web.App_Start;
using HeroicSupport.Web.Infrastructure.Mapping;
using NUnit.Framework;

namespace HeroicSupport.UnitTests.Web
{
	[SetUpFixture]
	public class WebSpecSetup
	{
		[SetUp]
		public void Init()
		{
			//Setup maps
			new MappingBootstrapper().Execute();

			//Setup routes
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}