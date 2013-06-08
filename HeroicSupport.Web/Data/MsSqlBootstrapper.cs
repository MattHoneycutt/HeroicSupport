using System.Configuration;
using FluentNHibernate.Cfg.Db;
using HeroicSupport.Core.Data;
using NHibernate;

namespace HeroicSupport.Web.Data
{
	public static class MsSqlBootstrapper
	{
		private static readonly NHibernateBootstrapper Bootstrapper = new NHibernateBootstrapper();

		public static void Bootstrap()
		{
			Bootstrap(ConfigurationManager.ConnectionStrings["HeroicSupportDatabase"].ConnectionString);
		}

		public static void Bootstrap(string connectionString)
		{
			Bootstrapper.Bootstrap(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString));
		}

		public static void CreateSchema()
		{
			Bootstrapper.CreateSchema();
		}

		public static void UpdateSchema()
		{
			Bootstrapper.UpdateSchema();
		}

		public static ISession GetSession()
		{
			return Bootstrapper.GetSession();
		}
	}
}