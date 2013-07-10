using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace HeroicSupport.Core.Data
{
	public class CascadeAllConvention : IHasOneConvention, IHasManyConvention, IReferenceConvention
	{
		public void Apply(IOneToOneInstance instance)
		{
			instance.Cascade.All();
		}

		public void Apply(IOneToManyCollectionInstance instance)
		{
			instance.Cascade.All();
		}

		public void Apply(IManyToOneInstance instance)
		{
			instance.Cascade.All();
		}
	}
}