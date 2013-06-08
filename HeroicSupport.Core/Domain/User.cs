using System;
using System.Security.Cryptography;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace HeroicSupport.Core.Domain
{
	public class User 
	{
		public virtual Guid UserID { get; set; }

		public virtual string DisplayName { get; set; }

		public virtual string EmailAddress { get; set; }

		public virtual string PasswordHash { get; set; }

		public virtual string PasswordSalt { get; protected set; }

		protected User()
		{

		}

		public static User CreateNewUser(string displayName, string emailAddress, string password)
		{
			var user = new User
				{
					DisplayName = displayName,
					EmailAddress = emailAddress
				};

			user.SetPassword(password);

			return user;
		}

		public virtual void SetPassword(string password)
		{
			GenerateNewSalt();

			PasswordHash = HashPassword(password);
		}

		public virtual bool IsThisTheUsersPassword(string password)
		{
			var hash = HashPassword(password);

			return hash == PasswordHash;
		}

		private string HashPassword(string password)
		{
			var hasher = MD5.Create();
			return Encoding.UTF8.GetString(hasher.ComputeHash(Encoding.UTF8.GetBytes(PasswordSalt + password)));
		}

		private void GenerateNewSalt()
		{
			var cryptoService = new RNGCryptoServiceProvider();
			byte[] buffer = new byte[10];
			cryptoService.GetBytes(buffer);
			PasswordSalt = Encoding.UTF8.GetString(buffer);
		}
	}

	public class UserOverrides : IAutoMappingOverride<User>
	{
		public void Override(AutoMapping<User> mapping)
		{
			mapping.Map(u => u.DisplayName).Unique();
			mapping.Map(u => u.EmailAddress).Unique();
		}		
	}
}