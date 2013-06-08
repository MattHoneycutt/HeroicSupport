using HeroicSupport.Core.Data;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace HeroicSupport.Web.Data
{
	public class DataRegistry : Registry
	{
		public DataRegistry()
		{
			For<ISession>().Use(MsSqlBootstrapper.GetSession);
			For(typeof(IRepository<>)).Use(typeof(NHibernateRepository<>));
		}
	}
}