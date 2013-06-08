using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.Mvc;

namespace HeroicSupport.Web.Helpers
{
	public static class GravatarExtensions
	{
		public static MvcHtmlString RenderGravatarImage(this HtmlHelper helper, string emailId, int imgSize)
		{
			// Convert emailID to lower-case
			emailId = emailId.ToLower();

			var hash = FormsAuthentication.HashPasswordForStoringInConfigFile(emailId, "MD5").ToLower();

			// build Gravatar Image URL
			var imageUrl = string.Format("http://www.gravatar.com/avatar/{0}?s={1}&d=mm&r=g", hash, imgSize);

			return helper.Image(imageUrl);
		}
	}
}