using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace HeroicSupport.Web.Models.Auth
{
	public class LoginForm
	{
		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }

	}
}