using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HeroicSupport.Core.Tasks;
using HeroicSupport.Web.App_Start;
using HeroicSupport.Web.Data;
using HeroicSupport.Web.Infrastructure;
using StructureMap;

namespace HeroicSupport.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : HeroicSupportWebApplicationBase
	{
		public MvcApplication()
		{
			var dependencyResolver = new StructureMapMvcDependencyResolver();
			DependencyResolver.SetResolver(dependencyResolver);
			GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
			GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;
		}

		protected override void ConfigureContainer(IContainer container)
		{
			container.Configure(cfg =>
			{
				cfg.AddRegistry(new StandardRegistry());
				cfg.AddRegistry(new TaskRegistry());
				cfg.AddRegistry(new ControllerRegistry());
				cfg.AddRegistry(new DataRegistry());
				cfg.AddRegistry(new MvcIntrinsicRegistry());
			});


			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			BundleTable.Bundles.Add(
				new ScriptBundle("~/scripts/jquery")
				.Include(
					"~/Scripts/jquery-{version}.js",
					"~/Scripts/jquery.validate.js",
					"~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js"
				));

			BundleTable.Bundles.Add(
				new ScriptBundle("~/scripts/misc")
					.Include("~/Scripts/knockout-{version}.js")
					.IncludeDirectory("~/Scripts/app/", "*.js", true));
		}
	}
}