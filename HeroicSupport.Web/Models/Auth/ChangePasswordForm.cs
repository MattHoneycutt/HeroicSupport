using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HeroicSupport.Web.Models.Auth
{
	public class ChangePasswordForm
	{
		[Required, DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required, DataType(DataType.Password), Compare("NewPasswordConfirmation")]
		public string NewPassword { get; set; }

		[Required, DataType(DataType.Password)]
		public string NewPasswordConfirmation { get; set; }
	}
}