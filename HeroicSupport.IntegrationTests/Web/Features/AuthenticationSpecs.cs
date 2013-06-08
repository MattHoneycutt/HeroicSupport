using HeroicSupport.Web.Controllers;
using HeroicSupport.Web.Models.Auth;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using MvcContrib.TestHelper;
using Should;

namespace HeroicSupport.IntegrationTests.Web.Features
{
	public abstract class AuthenticationSpecs : SpecsFor<MvcWebApp>
	{
		protected override void Given()
		{
			SUT.NavigateTo<AuthController>(c => c.Logout());
		}

		public class when_logging_in_with_no_credentials : AuthenticationSpecs
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthController>(c => c.Login());
				SUT.FindFormFor<LoginForm>()
					.Field(f => f.EmailAddress).SetValueTo(string.Empty)
					.Field(f => f.Password).SetValueTo(string.Empty)
					.Submit();
			}

			[Test]
			public void then_there_is_a_validation_error_on_the_email()
			{
				SUT.FindFormFor<LoginForm>()
				   .Field(f => f.EmailAddress).ShouldBeInvalid();
			}

			[Test]
			public void then_there_is_a_validation_error_on_the_password()
			{
				SUT.FindFormFor<LoginForm>()
				   .Field(f => f.Password).ShouldBeInvalid();
			}
		}

		public class when_logging_in_with_invalid_credentials : AuthenticationSpecs
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthController>(c => c.Login());
				SUT.FindFormFor<LoginForm>()
					.Field(f => f.EmailAddress).SetValueTo("baduser@test.com")
					.Field(f => f.Password).SetValueTo("NotAPassword")
					.Submit();
			}

			[Test]
			public void then_it_displays_a_validation_error()
			{
				SUT.ValidationSummary.Text.ShouldContain("invalid");
			}
		}

		public class when_logging_in_with_valid_credentials : AuthenticationSpecs
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthController>(c => c.Login());
				SUT.FindFormFor<LoginForm>()
					.Field(f => f.EmailAddress).SetValueTo("test@user.com")
					.Field(f => f.Password).SetValueTo("Password1")
					.Submit();
			}

			[Test]
			public void then_it_redirects_to_the_dashboard()
			{
				SUT.Route.ShouldMapTo<DashboardController>(c => c.Index());
			}
		}
	}
}