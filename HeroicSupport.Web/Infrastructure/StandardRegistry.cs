using StructureMap.Configuration.DSL;

namespace HeroicSupport.Web.Infrastructure
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			Scan(scan =>
				{
					scan.AssembliesFromApplicationBaseDirectory(asm => asm.FullName.Contains("HeroicSupport"));
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});
		}
	}
}