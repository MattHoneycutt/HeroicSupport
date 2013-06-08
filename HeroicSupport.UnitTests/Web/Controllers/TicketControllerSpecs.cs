using System;
using System.Web.Mvc;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using HeroicSupport.Core.Replies;
using HeroicSupport.UnitTests.Web.TestHelpers;
using HeroicSupport.Web.Controllers;
using HeroicSupport.Web.Infrastructure.Security;
using HeroicSupport.Web.Models.Dashboard;
using HeroicSupport.Web.Models.SubmitTicket;
using Moq;
using NUnit.Framework;
using SpecsFor;
using MvcContrib.TestHelper;
using SpecsFor.ShouldExtensions;
using Should;

namespace HeroicSupport.UnitTests.Web.Controllers
{
	public class TicketControllerSpecs
	{
		public class when_submitting_a_new_ticket : SpecsFor<TicketController>
		{
			private ActionResult _result;

			protected override void Given()
			{
				GetMockFor<ICurrentUser>()
					.Setup(u => u.User)
					.Returns(User.CreateNewUser("Test", "test@user.com", "Password!"));
			}

			protected override void When()
			{
				_result = SUT.Add(new AddForm
					{
						Subject = "Test ticket!",
						Body = "Test body!"
					});
			}

			[Test]
			public void then_it_should_save_a_new_ticket_to_the_repo()
			{
				GetMockFor<IRepository<Ticket>>()
					.Verify(r => r.Add(Looks.LikePartialOf<Ticket>(new
						{
							SubmittedBy = "test@user.com",
							Subject = "Test ticket!",
							Body = "Test body!"
						})));
			}

			[Test]
			public void then_it_should_redirect_to_the_dashboard()
			{
				_result.AssertActionRedirect().ToAction<DashboardController>(c => c.Index());
			}

			[Test]
			public void then_it_should_display_a_success_message()
			{
				SUT.ShouldHaveSuccessMessage();
			}
		}

		public class when_viewing_an_existing_ticket : SpecsFor<TicketController>
		{
			protected Ticket TestTicket;
			private ActionResult _result;

			protected override void Given()
			{
				TestTicket = Ticket.NewTicket("test@ticket.com", "Test Subject", "Test Body");
				TestTicket.TicketID = Guid.NewGuid();

				GetMockFor<IRepository<Ticket>>()
					.Setup(r => r.Get(TestTicket.TicketID))
					.Returns(TestTicket);
			}

			protected override void When()
			{
				_result = SUT.ViewTicket(TestTicket.TicketID);
			}

			[Test]
			public void then_the_ticket_details_are_displayed()
			{
				_result.ShouldBeType<ViewResult>()
					.Model.ShouldLookLike(new TicketViewModel
						{
							TicketID = TestTicket.TicketID,
							SubmittedBy = "test@ticket.com",
							Subject = "Test Subject",
							Body = "Test Body",
							DateReceived = TestTicket.DateReceived,
							LastUpdated = TestTicket.LastUpdated,
							Replies = new ReplyViewModel[0]
						});
			}
		}

		public abstract class given_a_ticket_exists : SpecsFor<TicketController>
		{
			protected Ticket TestTicket;

			protected override void Given()
			{
				GetMockFor<ICurrentUser>()
					.Setup(u => u.User)
					.Returns(User.CreateNewUser("Test", "test@user.com", "Test!"));

				TestTicket = Ticket.NewTicket("test@user.com", "Test Subject", "Test Body");
				TestTicket.TicketID = Guid.NewGuid();

				GetMockFor<IRepository<Ticket>>()
					.Setup(r => r.Get(TestTicket.TicketID))
					.Returns(TestTicket);
			}

			public class when_replying_to_a_ticket : given_a_ticket_exists
			{
				private ActionResult _result;

				protected override void When()
				{
					_result = SUT.Reply(new ReplyForm
					{
						TicketID = TestTicket.TicketID,
						Body = "This is a test reply!"
					});
				}

				[Test]
				public void then_it_redirects_to_the_view_page()
				{
					_result.AssertActionRedirect().ToAction<TicketController>(c => c.ViewTicket(TestTicket.TicketID));
				}

				[Test]
				public void then_it_displays_a_success_message()
				{
					SUT.ShouldHaveSuccessMessage();
				}

				[Test]
				public void then_it_adds_the_reply_to_the_ticket()
				{
					TestTicket.Replies[0].Body.ShouldEqual("This is a test reply!");
				}

				[Test]
				public void then_it_saves_the_changes()
				{
					GetMockFor<IRepository<Ticket>>()
						.Verify(r => r.Save());
				}
			}

			public class when_replying_to_a_ticket_and_an_error_occurs_while_sending_the_replly : given_a_ticket_exists
			{
				private ActionResult _result;

				protected override void Given()
				{
					base.Given();
					GetMockFor<IReplyNotifier>()
						.Setup(r => r.SendReply(TestTicket, It.IsAny<User>(), It.IsAny<string>()))
						.Throws<InvalidOperationException>();
				}

				protected override void When()
				{
					_result = SUT.Reply(new ReplyForm
					{
						TicketID = TestTicket.TicketID,
						Body = "This is a test reply!"
					});
				}

				[Test]
				public void then_it_redirects_to_the_view_page()
				{
					_result.AssertActionRedirect().ToAction<TicketController>(c => c.ViewTicket(TestTicket.TicketID));
				}

				[Test]
				public void then_it_displays_an_error_message()
				{
					SUT.ShouldHaveErrorMessage();
				}

				[Test]
				public void then_it_should_not_save_the_reply()
				{
					GetMockFor<IRepository<Ticket>>()
						.Verify(r => r.Save(), Times.Never());
				}
			}
		}
	}
}