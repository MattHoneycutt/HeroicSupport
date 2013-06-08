using System;
using System.Linq.Expressions;
using AutoMapper;

namespace HeroicSupport.Web.Infrastructure.Mapping
{
	public interface IMemberMappingExpression<T1, T2>
	{
		IMappingExpression<T1, T2> MapFrom<TResult>(Expression<Func<T1, TResult>> sourceMember);
		IMappingExpression<T1, T2> Ignore();
	}
}