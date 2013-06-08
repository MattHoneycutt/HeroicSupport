using System.ComponentModel.DataAnnotations;

namespace HeroicSupport.Web.Models.SubmitTicket
{
	public class AddForm
	{
		[Required]
		public string Subject { get; set; }

		[Required, DataType(DataType.MultilineText)]
		public string Body { get; set; }
	}
}