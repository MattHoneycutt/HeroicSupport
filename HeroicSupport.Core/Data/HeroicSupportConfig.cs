using System;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using HeroicSupport.Core.Domain;

namespace HeroicSupport.Core.Data
{
	public class HeroicSupportConfig : DefaultAutomappingConfiguration
	{
		public override bool IsId(Member member)
		{
			return member.Name == member.DeclaringType.Name + "ID";
		}

		public override bool ShouldMap(Type type)
		{
			if (type.Namespace != typeof(Ticket).Namespace)
			{
				return false;
			}

			return base.ShouldMap(type);
		}

		public override bool ShouldMap(Member member)
		{
			var prop = member.MemberInfo as PropertyInfo;

			if (prop != null && !prop.CanWrite)
			{
				return false;
			}

			return base.ShouldMap(member);
		}
	}
}