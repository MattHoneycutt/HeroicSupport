using HeroicSupport.Core.Domain;

namespace HeroicSupport.Core.Replies
{
	public interface IReplyNotifier
	{
		void SendReply(Ticket ticket, User user, string reply);
	}
}