using HeroicSupport.Core.Tasks;

namespace HeroicSupport.Web.Data
{
	public class DatabaseInit : IRunAtInit
	{
		public void Execute()
		{
			MsSqlBootstrapper.Bootstrap();
			//You can uncomment this if you need to throw away and recreate the schema
			//MsSqlBootstrapper.CreateSchema();
			MsSqlBootstrapper.UpdateSchema();
		}
	}
}