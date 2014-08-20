using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.Controllers
{
	public class SeanceController : Controller
	{
		private readonly ISeanceService _seanceService;
		private readonly IHallService _hallService;

		public SeanceController(ISeanceService seanceService, IHallService hallService)
		{
			if (seanceService == null)
			{
				throw new ArgumentNullException("seanceService");
			}
			if (hallService == null)
			{
				throw new ArgumentNullException("hallService");
			}

			_seanceService = seanceService;
			_hallService = hallService;
		}

		public ActionResult Index(long filmId)
		{
			ViewBag.FilmId = filmId;

			return View("Index");
		}

		public ActionResult List()
		{
			var seancesDomainModel = _seanceService.Read();
			var seancesViewModel = Mapper.Map<IEnumerable<SeanceDomainModel>, IEnumerable<SeanceViewModel>>(seancesDomainModel);
			
			return PartialView("_ListPartial", seancesViewModel);
		}

		public ActionResult Create()
		{
			var hallsCinemas = _hallService.GetHallsCinemas();
			ViewBag.HallsCinemas = hallsCinemas;
			
			return PartialView("_CreatePartial");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(SeanceViewModel seanceViewModel)
		{
			return View();			
		}

		public ActionResult Edit(long id)
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(long id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Details(long id)
		{
			return View();
		}

		public ActionResult Delete(long id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult Delete(long id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
