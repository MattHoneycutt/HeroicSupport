using System;
using System.Collections.Generic;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace HeroicSupport.Core.Domain
{
	public class Ticket
	{
		public static Ticket NewTicket(string submitterEmail, string subject, string body)
		{
			return new Ticket
				{
					DateReceived = DateTime.Now,
					LastUpdated = DateTime.Now,
					SubmittedBy = submitterEmail,
					Subject = subject,
					Body = body
				};
		}

		public virtual Guid TicketID { get; set; }

		public virtual string SubmittedBy { get; set; }

		public virtual string Subject { get; set; }

		public virtual DateTime DateReceived { get; set; }

		public virtual DateTime LastUpdated { get; set; }

		public virtual string Body { get; set; }

		public virtual string Tags { get; set; }

		public virtual IList<Reply> Replies { get; set; }

		protected Ticket()
		{
			Replies = new List<Reply>();
		}

		public virtual void AddReply(User by, string body)
		{
			LastUpdated = DateTime.Now;
			Replies.Add(new Reply
				{
					User = by,
					Body = body,
					SentAt = DateTime.Now
				});
		}
	}

	public class TicketOverrides : IAutoMappingOverride<Ticket>
	{
		public void Override(AutoMapping<Ticket> mapping)
		{
			mapping.Map(t => t.Body).Length(10000);
		}
	}
}