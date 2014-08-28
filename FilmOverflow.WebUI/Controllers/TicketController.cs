using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.Attributes;
using FilmOverflow.WebUI.ViewModels;
using Microsoft.AspNet.Identity;

namespace FilmOverflow.WebUI.Controllers
{
	public class TicketController : Controller
	{
		private readonly ITicketService _ticketService;
		private readonly IPaymentMethodService _paymentMethodService;
		private readonly ISeanceService _seanceService;

		public TicketController(ITicketService ticketService, IPaymentMethodService paymentMethodService, ISeanceService seanceService)
		{
			_ticketService = ticketService;
			_paymentMethodService = paymentMethodService;
			_seanceService = seanceService;
		}

		public ActionResult Index()
		{
			var applicationUserId = User.Identity.GetUserId();
			IEnumerable<TicketDomainModel> ticketDomainModel = _ticketService
				.Read()
				.Where(ticket => ticket.ApplicationUserId == applicationUserId);
			IEnumerable<TicketViewModel> ticketViewModel =
				(Mapper.Map<IEnumerable<TicketDomainModel>, IEnumerable<TicketViewModel>>(ticketDomainModel)).ToList();

			return View(ticketViewModel);
		}

		[Authorize]
		public ActionResult Create(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			SeanceDomainModel currentSeance = _seanceService.ReadById(id);
			if (currentSeance == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			ViewBag.SeanceId = id;
			ViewBag.Hall = currentSeance.Hall;
			ViewBag.PaymentMethods = _paymentMethodService.GetSelectListItems();
			return View();
		}

		[HttpPost]
		public ActionResult Create([FromJson] OrderViewModel orderViewModel)
		{
			ModelState.Clear();
			string currentUserId = User.Identity.GetUserId();

			var permittedUserSeats = (from seat in _seanceService.ReadById(orderViewModel.SeanceId).ReservedSeats
									  where seat.ApplicationUserId == currentUserId
										  && seat.IsSold == false
										  && DateTime.Compare(DateTime.Now, seat.ReservationTime.AddSeconds(10)) < 0
									  select seat).ToList();

			List<SeatViewModel> targetSeats = new List<SeatViewModel>();

			foreach (var seat in permittedUserSeats)
			{
				SeatViewModel seatModel = (from seatToBuy in orderViewModel.Seats
										   where seatToBuy.ColumnNumber == seat.ColumnNumber
											   && seatToBuy.RowNumber == seat.RowNumber

										   select seatToBuy).SingleOrDefault();
				if (seatModel != null)
				{
					targetSeats.Add(seatModel);
				}
			}
			bool areAllSeatsAccepted = orderViewModel.Seats.Count() == targetSeats.Count();
			if (areAllSeatsAccepted)
			{
				
				foreach (var seat in targetSeats)
				{
					TicketDomainModel ticket = new TicketDomainModel();
					ticket.SeatId = seat.Id;
					ticket.ApplicationUserId = currentUserId;
					ticket.PaymentDate = DateTime.Now;
					ticket.PaymentMethodId = orderViewModel.PaymentMethodId;
					ticket.SeanceId = orderViewModel.SeanceId;
					_ticketService.Add(ticket);
				}
				return Json(Url.Action("Index", "Ticket"));
			}
			return Json(Url.Action("TimeIsOut", "Home"));
		}
	}
}
