using System.Collections.Generic;

namespace HeroicSupport.Web.Infrastructure.Mapping
{
	public interface IMapperExpression
	{
		IEnumerable<TDestination> To<TDestination>();
	}
}