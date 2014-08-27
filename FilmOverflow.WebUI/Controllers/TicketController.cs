using System;
using System.Collections.Generic;
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
			ViewBag.PaymentMethods = _paymentMethodService.GetSelectListItems();
			return View();
		}

		[HttpPost]
		public ActionResult Create([FromJson] OrderViewModel orderViewModel)
		{
			if (!ModelState.IsValid)
			{
				//return View(ticketViewModel);
			}
			//TicketDomainModel ticketDomainModel = Mapper.Map<TicketViewModel, TicketDomainModel>(ticketViewModel);
			//ticketDomainModel.PaymentDate = DateTime.Now;
			//ticketDomainModel.ApplicationUserId = User.Identity.GetUserId();
			//_ticketService.Add(ticketDomainModel);
			return RedirectToAction("Index");
		}
	}
}
