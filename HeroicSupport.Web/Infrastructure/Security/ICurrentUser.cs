using HeroicSupport.Core.Domain;

namespace HeroicSupport.Web.Infrastructure.Security
{
	public interface ICurrentUser
	{
		User User { get; }
	}
}