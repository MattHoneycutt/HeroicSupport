using System;
using System.Web.Mvc;
using AutoMapper;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using HeroicSupport.Core.Replies;
using HeroicSupport.Web.Filters;
using HeroicSupport.Web.Infrastructure.Security;
using HeroicSupport.Web.Models.Dashboard;
using HeroicSupport.Web.Models.SubmitTicket;
using Microsoft.Web.Mvc;
using HeroicSupport.Web.Helpers;

namespace HeroicSupport.Web.Controllers
{
    public class TicketController : Controller
    {
	    private readonly IRepository<Ticket> _tickets;
	    private readonly IReplyNotifier _notifier;
	    private readonly ICurrentUser _user;

	    public TicketController(IRepository<Ticket> tickets, IReplyNotifier notifier, ICurrentUser user)
	    {
		    _tickets = tickets;
		    _notifier = notifier;
		    _user = user;
	    }

	    public ActionResult Add()
	    {
		    return View();
	    }

		[HttpPost]
		[StandardModelStateValidation]
		public ActionResult Add(AddForm form)
		{
			var ticket = Ticket.NewTicket(_user.User.EmailAddress, form.Subject, form.Body);

			_tickets.Add(ticket);
			_tickets.Save();

			this.Success("Ticket submitted successfully!");
			return this.RedirectToAction<DashboardController>(c => c.Index());
		}

	    public ActionResult ViewTicket(Guid id)
	    {
		    var ticket = _tickets.Get(id);

		    return View(Mapper.Map<TicketViewModel>(ticket));
	    }

		[HttpPost, AllowAnonymous, ValidateInput(false)]
		public ActionResult FromMail(MailinData data)
		{
			var ticket = Ticket.NewTicket(data.Envelope.From, data.Headers.Subject, data.Plain);

			_tickets.Add(ticket);
			_tickets.Save();


			return Content("Ticket created!");
		}

		[ChildActionOnly]
	    public ActionResult Reply(Guid ticketID)
		{
			return View(new ReplyForm {TicketID = ticketID});
		}

		[HttpPost, StandardModelStateValidation]
	    public ActionResult Reply(ReplyForm form)
		{
			var ticket = _tickets.Get(form.TicketID);
			ticket.AddReply(_user.User, form.Body);

			//send the message to the user
			try
			{
				_notifier.SendReply(ticket, _user.User, form.Body);
			}
			catch
			{
				this.Error("There was a problem sending an E-mail to the user.  Please try again.");
				return this.RedirectToAction(c => c.ViewTicket(form.TicketID));
			}

			_tickets.Save();

			this.Success("Reply sent!");
			return this.RedirectToAction(c => c.ViewTicket(form.TicketID));
		}

		[HttpPost]
	    public ActionResult SaveTags(Guid? ticketID, string tags)
		{
			var ticket = _tickets.Get(ticketID);
			ticket.Tags = tags;

			_tickets.Save();

			return Json(true);
	    }
    }
}
