using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HeroicSupport.Core.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace HeroicSupport.Core.Data
{
	public class NHibernateBootstrapper
	{
		private ISessionFactory _sessionFactory;
		private Configuration _configuration;

		public void Bootstrap(IPersistenceConfigurer databaseConfiguration)
		{
			_configuration = Fluently.Configure()
				.Database(databaseConfiguration)
				.Mappings(m => m.AutoMappings.Add(
					AutoMap.AssemblyOf<Ticket>(new HeroicSupportConfig())
						.UseOverridesFromAssemblyOf<Ticket>()
						.Conventions.AddFromAssemblyOf<Ticket>()
					)
				)
				.ExposeConfiguration(cfg => cfg.SetProperty("connection.release_mode", "on_close"))
				.BuildConfiguration();

			_sessionFactory = _configuration.BuildSessionFactory();
		}

		public void UpdateSchema()
		{
			new SchemaUpdate(_configuration)
				.Execute(false, true);
		}

		public void CreateSchema()
		{
			new SchemaExport(_configuration)
				.Execute(false, true, false);
		}

		public ISession GetSession()
		{
			return _sessionFactory.OpenSession();
		}
	}
}