using System;
using HeroicSupport.Core.Domain;
using HeroicSupport.Web.Infrastructure.Mapping;

namespace HeroicSupport.Web.Models.Dashboard
{
	public class ReplyViewModel : IMapFrom<Reply>
	{
		public string Body { get; set; }

		public string UserEmailAddress { get; set; }

		public DateTime SentAt { get; set; }
	}
}