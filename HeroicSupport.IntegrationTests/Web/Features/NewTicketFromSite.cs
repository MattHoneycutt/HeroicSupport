using HeroicSupport.Web.Controllers;
using HeroicSupport.Web.Models.Dashboard;
using HeroicSupport.Web.Models.SubmitTicket;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using MvcContrib.TestHelper;
using Should;
using SpecsFor.Mvc.SeleniumExtensions;

namespace HeroicSupport.IntegrationTests.Web.Features
{
	public class NewTicketFromSite
	{
		public class when_a_new_ticket_is_submitted : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<DashboardController>(c => c.Index());
				SUT.FindLinkTo<TicketController>(c => c.Add()).ClickButton();
				SUT.FindFormFor<AddForm>()
				   .Field(f => f.Subject).SetValueTo("This is a test ticket!")
				   .Field(f => f.Body).SetValueTo("This is the body for the test ticket! ")
				   .Submit();
			}

			[Test]
			public void then_it_redirects_to_the_dashboard()
			{
				SUT.Route.ShouldMapTo<DashboardController>(c => c.Index());
			}

			[Test]
			public void then_it_shows_the_new_ticket_on_the_dashboard()
			{
				SUT.FindDisplayFor<DashboardViewModel>()
					.DisplayFor(m => m.Tickets[0].Subject).Text.ShouldEqual("This is a test ticket!");
			}
		}
	}
}