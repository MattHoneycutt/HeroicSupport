using System;
using HeroicSupport.Core.Domain;
using HeroicSupport.Web.Infrastructure.Mapping;

namespace HeroicSupport.Web.Models.Dashboard
{
	public class TicketViewModel : IMapFrom<Ticket>
	{
		public Guid TicketID { get; set; }

		public string SubmittedBy { get; set; }

		public string Subject { get; set; }

		public DateTime LastUpdated { get; set; }

		public string Body { get; set; }

		public DateTime DateReceived { get; set; }

		public ReplyViewModel[] Replies { get; set; }

		public string Tags { get; set; }
	}
}