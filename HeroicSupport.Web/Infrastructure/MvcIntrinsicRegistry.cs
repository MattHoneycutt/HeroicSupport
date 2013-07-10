﻿using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap.Configuration.DSL;

namespace HeroicSupport.Web.Infrastructure
{
	public class MvcIntrinsicRegistry : Registry
	{
		public MvcIntrinsicRegistry()
		{
			For<BundleCollection>().Use(BundleTable.Bundles);
			For<RouteCollection>().Use(RouteTable.Routes);
			For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
			For<HttpSessionStateBase>().Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
			For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
			For<HttpServerUtilityBase>().Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
		}
	}
}