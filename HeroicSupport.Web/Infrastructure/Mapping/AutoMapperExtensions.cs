using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace HeroicSupport.Web.Infrastructure.Mapping
{
	public static class AutoMapperExtensions
	{
		public static IMapperExpression Map<TSource>(this IEnumerable<TSource> source)
		{
			return new MapperExpression<TSource>(source);
		}

		public static IMappingExpression IgnoreAllNonExisting(this IMappingExpression expression, Type sourceType, Type destinationType)
		{
			var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
			foreach (var property in existingMaps.GetUnmappedPropertyNames())
			{
				expression.ForMember(property, opt => opt.Ignore());
			}
			return expression;
		}

		public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
		{
			var sourceType = typeof(TSource);
			var destinationType = typeof(TDestination);
			var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
			foreach (var property in existingMaps.GetUnmappedPropertyNames())
			{
				expression.ForMember(property, opt => opt.Ignore());
			}
			return expression;
		}

		public static MemberMappingExpression<T1, T2> ForMember<T1, T2>(this IMappingExpression<T1, T2> expression, Expression<Func<T2, object>> member)
		{
			return new MemberMappingExpression<T1, T2>(expression, member);
		}
	}
}