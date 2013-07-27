using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace HeroicSupport.Web.Helpers
{
	public static class LinkHelpers
	{
		public static MvcHtmlString MenuLink<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string linkText) where TController : Controller
		{
			var linkRoute = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);

			var currentRoute = helper.ViewContext.RouteData.Values;

			var actionLink = helper.ActionLink(action, linkText);

			var isCurrentRoute = linkRoute["controller"].ToString() == currentRoute["controller"].ToString() &&
			                     linkRoute["action"].ToString() == currentRoute["action"].ToString();

			return MvcHtmlString.Create(string.Format("<li{0}>", isCurrentRoute ? " class=\"active\"" : string.Empty) + actionLink + "</li>");
		}
	}
}