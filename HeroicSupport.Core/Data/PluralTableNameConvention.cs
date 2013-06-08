using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using HeroicSupport.Core.Utility;

namespace HeroicSupport.Core.Data
{
	public class PluralTableNameConvention : IClassConvention
	{
		public void Apply(IClassInstance instance)
		{
			instance.Table(Inflector.Pluralize(instance.EntityType.Name));
		}
	}
}