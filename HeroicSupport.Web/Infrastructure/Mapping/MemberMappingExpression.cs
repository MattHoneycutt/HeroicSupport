using System;
using System.Linq.Expressions;
using AutoMapper;

namespace HeroicSupport.Web.Infrastructure.Mapping
{
	public class MemberMappingExpression<T1, T2> : IMemberMappingExpression<T1, T2>
	{
		private readonly IMappingExpression<T1, T2> _mappingExpression;
		private readonly Expression<Func<T2, object>> _member;

		public MemberMappingExpression(IMappingExpression<T1, T2> mappingExpression, Expression<Func<T2, object>> member)
		{
			_mappingExpression = mappingExpression;
			_member = member;
		}

		public IMappingExpression<T1, T2> MapFrom<TResult>(Expression<Func<T1, TResult>> sourceMember)
		{
			_mappingExpression.ForMember(_member, opt => opt.MapFrom(sourceMember));

			return _mappingExpression;
		}

		public IMappingExpression<T1, T2> Ignore()
		{
			_mappingExpression.ForMember(_member, opt => opt.Ignore());

			return _mappingExpression;
		}
	}
}