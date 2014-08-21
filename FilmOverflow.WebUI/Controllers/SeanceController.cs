using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

		public ActionResult List(long filmId)
		{
			var seancesDomainModel = _seanceService
				.Read()
				.Where(x => x.FilmId == filmId);
			var seancesViewModel = Mapper.Map<IEnumerable<SeanceDomainModel>, IEnumerable<SeanceViewModel>>(seancesDomainModel);

			return PartialView("_ListPartial", seancesViewModel);
		}

		public ActionResult Create(long filmId)
		{
			IEnumerable<SelectListItem> hallsCinemas = _hallService
				.Read()
				.Select(x => new SelectListItem()
			{
				Value = x.Id.ToString(),
				Text = x.Cinema.Name + " " + x.Name
			});

			ViewBag.HallsCinemas = hallsCinemas;
			ViewBag.FilmId = filmId;

			return PartialView("_CreatePartial");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(SeanceViewModel seanceViewModel)
		{
			if (seanceViewModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (!ModelState.IsValid)
			{
				return PartialView("_CreatePartial", seanceViewModel);
			}

			SeanceDomainModel seanceDomainModel = Mapper.Map<SeanceViewModel, SeanceDomainModel>(seanceViewModel);
			_seanceService.Add(seanceDomainModel);

			var url = Url.Action("List", "Seance", routeValues: new { filmId = seanceViewModel.FilmId });
			return Json(new { success = true, url = url, replaceTarget = "#SeanceList" });
		}

		public ActionResult Edit(long? id)
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(SeanceViewModel seanceViewModel)
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

		public ActionResult Details(long? id)
		{
			return View();
		}

		public ActionResult Delete(long? id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult Delete(long id)
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
