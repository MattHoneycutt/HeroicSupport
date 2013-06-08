using StructureMap.Configuration.DSL;

namespace HeroicSupport.Core.Tasks
{
	public class TaskRegistry : Registry
	{
		public TaskRegistry()
		{
			Scan(x =>
			     	{
						x.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("Heroic"));
			     		x.AddAllTypesOf<IRunAtInit>();
			     		x.AddAllTypesOf<IRunAtStartup>();
			     	});
		}
	}
}