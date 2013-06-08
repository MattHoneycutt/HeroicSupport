using System.Linq;
using System.Web;
using HeroicSupport.Core.Tasks;
using StructureMap;

namespace HeroicSupport.Web.Infrastructure
{
	public abstract class HeroicSupportWebApplicationBase : HttpApplication
	{
		public static IContainer Container
		{
			get { return (IContainer)HttpContext.Current.Items["_Container"]; }
			private set { HttpContext.Current.Items["_Container"] = value; }
		}

		protected abstract void ConfigureContainer(IContainer container);

		protected void Application_Start()
		{
			ConfigureContainer(ObjectFactory.Container);

			using (var container = ObjectFactory.Container.GetNestedContainer())
			{
				foreach (var task in container.GetAllInstances<IRunAtInit>().OrderBy(t => t.GetType().FullName))
				{
					task.Execute();
				}

				foreach (var task in container.GetAllInstances<IRunAtStartup>().OrderBy(t => t.GetType().FullName))
				{
					task.Execute();
				}
			}
		}

		protected void Application_BeginRequest()
		{
			Container = ObjectFactory.Container.GetNestedContainer();

			foreach (var task in Container.GetAllInstances<IRunOnEachRequest>().OrderBy(t => t.GetType().FullName))
			{
				task.Execute();
			}
		}

		protected void Application_Error()
		{
			foreach (var task in Container.GetAllInstances<IRunOnError>().OrderBy(t => t.GetType().FullName))
			{
				task.Execute();
			}
		}

		protected void Application_EndRequest()
		{
			try
			{
				foreach (var task in Container.GetAllInstances<IRunAfterEachRequest>().OrderBy(t => t.GetType().FullName))
				{
					task.Execute();
				}
			}
			finally
			{
				Container.Dispose();
				Container = null;
			}
		}
	}
}