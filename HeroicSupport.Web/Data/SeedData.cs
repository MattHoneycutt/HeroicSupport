using System.Linq;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using HeroicSupport.Core.Tasks;

namespace HeroicSupport.Web.Data
{
	public class SeedData : IRunAtStartup
	{
		private readonly IRepository<User> _users;

		public SeedData(IRepository<User> users)
		{
			_users = users;
		}

		public void Execute()
		{
			if (!_users.Query().ToList().Any())
			{
				_users.Add(User.CreateNewUser("admin", "admin@notarealdomain.com", "admin123"));
				_users.Save();
			}
		}
	}
}