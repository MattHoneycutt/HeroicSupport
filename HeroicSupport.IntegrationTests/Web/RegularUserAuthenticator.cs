using HeroicSupport.Web.Controllers;
using HeroicSupport.Web.Models.Auth;
using SpecsFor.Mvc;
using SpecsFor.Mvc.Authentication;

namespace HeroicSupport.IntegrationTests.Web
{
	public class RegularUserAuthenticator : IHandleAuthentication
	{
		public void Authenticate(MvcWebApp mvcWebApp)
		{
			mvcWebApp.NavigateTo<AuthController>(c => c.Login());
			mvcWebApp.FindFormFor<LoginForm>()
			         .Field(f => f.EmailAddress).SetValueTo("test@user.com")
			         .Field(f => f.Password).SetValueTo("Password1")
			         .Submit();
		}
	}
}