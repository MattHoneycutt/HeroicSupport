using System.Web.Optimization;
using HeroicSupport.Web.App_Start;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(BootstrapBundleConfig), "RegisterBundles")]

namespace HeroicSupport.Web.App_Start
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
			// Add @Styles.Render("~/Content/bootstrap") in the <head/> of your _Layout.cshtml view
			// Add @Scripts.Render("~/bundles/bootstrap") after jQuery in your _Layout.cshtml view
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/bootstrap*"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap")
				.Include(
				"~/Content/bootstrap.css",
				"~/Content/bootstrap-responsive.css",
				"~/Content/cerulean.css",
				"~/Content/bootstrap-tagmanager.css",
				"~/Content/body.css"));
		}
	}
}
