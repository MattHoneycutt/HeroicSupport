using System.Net.Mail;
using HeroicSupport.Core.Domain;

namespace HeroicSupport.Core.Replies
{
	public class ReplyNotifier : IReplyNotifier
	{
		public void SendReply(Ticket ticket, User user, string reply)
		{
			var message = new MailMessage(user.EmailAddress, ticket.SubmittedBy);
			message.Subject = "[Heroic Support] RE: " + ticket.Subject;
			message.Body = reply;

			var client = new SmtpClient();
			client.Send(message);
		}
	}
}