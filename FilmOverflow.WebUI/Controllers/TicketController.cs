using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;
using Microsoft.AspNet.Identity;

namespace FilmOverflow.WebUI.Controllers
{
	public class TicketController : Controller
	{
		private readonly ITicketService _ticketService;
		private readonly IPaymentMethodService _paymentMethodService;

		public TicketController(ITicketService ticketService, IPaymentMethodService paymentMethodService)
		{
			_ticketService = ticketService;
			_paymentMethodService = paymentMethodService;
		}

		public ActionResult Index()
		{
			IEnumerable<TicketDomainModel> ticketDomainModel = _ticketService.Read();
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
			ViewBag.SeanceId = id;
			ViewBag.PaymentMethods = _paymentMethodService.GetSelectListItems();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "SeanceId,PaymentMethodId")] TicketViewModel ticketViewModel)
		{

			if (!ModelState.IsValid)
			{
				return View(ticketViewModel);
			}
			TicketDomainModel ticketDomainModel = Mapper.Map<TicketViewModel, TicketDomainModel>(ticketViewModel);
			ticketDomainModel.PaymentDate = DateTime.Now;
			ticketDomainModel.ApplicationUserId = User.Identity.GetUserId();
			_ticketService.Add(ticketDomainModel);
			return RedirectToAction("Index");
		}
	}
}
