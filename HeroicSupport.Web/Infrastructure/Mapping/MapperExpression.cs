using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace HeroicSupport.Web.Infrastructure.Mapping
{
	public class MapperExpression<TSource> : IMapperExpression
	{
		private readonly IEnumerable<TSource> _source;

		public MapperExpression(IEnumerable<TSource> source)
		{
			_source = source;
		}

		public IEnumerable<TDestination> To<TDestination>()
		{
			return _source.Select(Mapper.Map<TSource, TDestination>);
		}
	}
}