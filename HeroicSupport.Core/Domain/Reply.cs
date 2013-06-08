using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace HeroicSupport.Core.Domain
{
	public class Reply
	{
		public virtual Guid ReplyID { get; set; }

		public virtual string Body { get; set; }

		public virtual User User { get; set; }

		public virtual DateTime SentAt { get; set; }
	}

	public class ReplyOverrides : IAutoMappingOverride<Reply>
	{
		public void Override(AutoMapping<Reply> mapping)
		{
			mapping.Map(r => r.Body).Length(10000);
		}
	}
}