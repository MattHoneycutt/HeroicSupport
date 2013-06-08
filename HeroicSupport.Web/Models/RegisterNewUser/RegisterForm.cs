using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HeroicSupport.Web.Models.RegisterNewUser
{
	public class RegisterForm
	{
		[Required]
		public string DisplayName { get; set; }

		[Required]
		[Microsoft.Web.Mvc.EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("PasswordConfirmation")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string PasswordConfirmation { get; set; }
	}
}