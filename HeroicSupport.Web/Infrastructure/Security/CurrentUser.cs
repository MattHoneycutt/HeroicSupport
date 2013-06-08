using System.Security.Principal;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using System.Linq;

namespace HeroicSupport.Web.Infrastructure.Security
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IIdentity _identity;
		private readonly IRepository<User> _users;
		private User _currentUser;

		public CurrentUser(IIdentity identity, IRepository<User> users)
		{
			_identity = identity;
			_users = users;
		}

		public User User 
		{ 
			get { return _currentUser = _currentUser ?? _users.Query().SingleOrDefault(u => u.EmailAddress == _identity.Name); } 
		}
	}
}