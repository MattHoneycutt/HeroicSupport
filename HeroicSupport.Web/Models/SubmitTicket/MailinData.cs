namespace HeroicSupport.Web.Models.SubmitTicket
{
	public class MailinData
	{
		public class MailHeaders
		{
			public string From { get; set; }

			public string Subject { get; set; }
		}

		public class MailEnvelope
		{
			public string From { get; set; }
		}

		public MailEnvelope Envelope { get; set; }

		public MailHeaders Headers { get; set; }

		public string Plain { get; set; }

		public string Html { get; set; }

		public string Reply_Plain { get; set; }
	}
}