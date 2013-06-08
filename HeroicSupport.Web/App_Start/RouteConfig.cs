using System.Web.Mvc;
using System.Web.Routing;

namespace HeroicSupport.Web.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//http://heroicsupport.apphb.com/mailticket
			routes.MapRoute(
				name: "MailTicket",
				url: "mailticket",
				defaults: new { controller = "Ticket", action = "FromMail" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}