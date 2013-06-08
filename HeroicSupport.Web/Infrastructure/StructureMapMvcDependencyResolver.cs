using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace HeroicSupport.Web.Infrastructure
{
	public class StructureMapMvcDependencyResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
	{
		private static IContainer GetContainer()
		{
			return HeroicSupportWebApplicationBase.Container ?? ObjectFactory.Container;
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == null) return null;

			var container = GetContainer();

			return serviceType.IsAbstract || serviceType.IsInterface
				       ? container.TryGetInstance(serviceType)
				       : container.GetInstance(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return GetContainer().GetAllInstances(serviceType).Cast<object>();
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public void Dispose()
		{
		}
	}
}