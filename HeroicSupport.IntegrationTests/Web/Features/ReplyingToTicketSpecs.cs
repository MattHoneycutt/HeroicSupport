using System.Net.Http;
using HeroicSupport.Web.Controllers;
using HeroicSupport.Web.Models.Dashboard;
using HeroicSupport.Web.Models.SubmitTicket;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecsFor;
using SpecsFor.Mvc;
using SpecsFor.Mvc.Smtp;
using Should;

namespace HeroicSupport.IntegrationTests.Web.Features
{
	public class ReplyingToTicketSpecs
	{
		public class when_replying_to_an_existing_ticket : SpecsFor<MvcWebApp>
		{
			protected override void Given()
			{
				var postUrl = MvcWebApp.BaseUrl + "/mailticket";

				var ticketData = new MailinData
					{
						Envelope = new MailinData.MailEnvelope{From = "some@user.com"},
						Headers = new MailinData.MailHeaders{Subject = "I need help!!"},
						Plain = "My weather widget is broken!",
					};

				var client = new HttpClient();
				client.PostAsJsonAsync(postUrl, ticketData).Wait();
			}

			protected override void When()
			{
				SUT.NavigateTo<DashboardController>(c => c.Index());
				SUT.Browser.FindElement(By.LinkText("View")).Click();
				SUT.FindFormFor<ReplyForm>()
				   .Field(f => f.Body).SetValueTo("Have you tried looking out the window?")
				   .Submit();
			}

			[Test]
			public void then_it_sends_an_email_back_to_the_user_with_the_reply()
			{
				SUT.Mailbox().MailMessages[0].To[0].Address.ShouldEqual("some@user.com");
				SUT.Mailbox().MailMessages[0].Body.ShouldContain("Have you tried looking out the window?");
			}

			[Test]
			public void then_it_displays_the_reply_on_the_page()
			{
				var display = SUT.FindDisplayFor<TicketViewModel>();
				display.DisplayFor(m => m.Replies[0].UserEmailAddress).Text.ShouldEqual("test@user.com");
				display.DisplayFor(m => m.Replies[0].Body).Text.ShouldEqual("Have you tried looking out the window?");
			}

			[Test]
			public void then_it_displays_a_success_message()
			{
				SUT.SuccessMessage().ShouldContain("Reply sent!");
			}
		}
	}
}