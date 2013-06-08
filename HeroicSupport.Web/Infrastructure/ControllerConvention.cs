using System;
using System.Web.Http;
using System.Web.Mvc;
using HeroicSupport.Core.Utility;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;

namespace HeroicSupport.Web.Infrastructure
{
	public class ControllerConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if ((type.CanBeCastTo<Controller>() || type.CanBeCastTo<ApiController>()) && !type.IsAbstract)
			{
				registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
			}
		}
	}
}