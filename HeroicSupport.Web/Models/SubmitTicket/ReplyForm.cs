using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HeroicSupport.Web.Models.SubmitTicket
{
	public class ReplyForm
	{
		[HiddenInput]
		public Guid TicketID { get; set; }

		[Required, DataType(DataType.MultilineText)]
		public string Body { get; set; }
	}
}