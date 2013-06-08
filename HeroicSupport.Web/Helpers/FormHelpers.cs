using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HeroicSupport.Web.Helpers
{
	public static class FormHelpers
	{
		 public static MvcHtmlString HorizontalFormForModel<TModel>(this HtmlHelper<TModel> helper, string submitButtonText = "Add")
		 {
			 return helper.EditorForModel("HorizontalForm", new{_SubmitButton = submitButtonText});
		 }
	}
}