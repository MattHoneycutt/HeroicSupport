using System;
using System.Web.Mvc;
using HeroicSupport.Core.Data;
using HeroicSupport.Core.Domain;
using HeroicSupport.Web.Models.Dashboard;
using AutoMapper.QueryableExtensions;
using System.Linq;
using HeroicSupport.Web.Infrastructure.Mapping;

namespace HeroicSupport.Web.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IRepository<Ticket> _tickets;

		public DashboardController(IRepository<Ticket> tickets)
		{
			_tickets = tickets;
		}

		public ActionResult Index()
		{
			//TODO: Eventually, determine which dashboard they get and send them to the correct one. 

			var ticketViewModels = _tickets.Query().Map().To<TicketViewModel>().ToArray();

			if (ticketViewModels.Length > 0)
			{
				return View(new DashboardViewModel
					{
						Tickets = ticketViewModels
					});
			}
			else
			{
				return View("NoTickets");
			}
		}
	}
}
