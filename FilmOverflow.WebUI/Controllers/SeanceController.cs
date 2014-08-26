using System;
using System.Collections.Generic;
using System.Globalization;
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
			ViewBag.FilmId = filmId;

			return PartialView("_ListPartial", seancesViewModel);
		}

		public ActionResult Create(long filmId)
		{
			IEnumerable<SelectListItem> hallsCinemas = GetHallsCinemas();
			ViewBag.HallsCinemas = hallsCinemas;
			ViewBag.FilmId = filmId;

			return PartialView("_CreatePartial");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(SeanceViewModel seanceViewModel)
		{
			//var dateString = seanceViewModel.Date;
			//var timeString = seanceViewModel.Time;
			//var date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			//var time = DateTime.Today.Add(TimeSpan.Parse(timeString));

			//Console.Write(date);

			if (seanceViewModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (!ModelState.IsValid)
			{
				ViewBag.HallsCinemas = GetHallsCinemas();
				return PartialView("_CreatePartial", seanceViewModel);
			}

			SeanceDomainModel seanceDomainModel = Mapper.Map<SeanceViewModel, SeanceDomainModel>(seanceViewModel);
			_seanceService.Add(seanceDomainModel);

			var url = Url.Action("List", "Seance", routeValues: new { filmId = seanceViewModel.FilmId });
			return Json(new { success = true, url = url, replaceTarget = "#SeanceList" });
		}

		public ActionResult Edit(long? seanceId)
		{
			if (seanceId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			SeanceDomainModel seanceDomianModel = _seanceService.ReadById(seanceId);
			SeanceViewModel seanceViewModel = Mapper.Map<SeanceDomainModel, SeanceViewModel>(seanceDomianModel);
			
			if (seanceViewModel == null)
			{
				return HttpNotFound();
			}

			IEnumerable<SelectListItem> hallsCinemas = GetHallsCinemas();
			ViewBag.HallsCinemas = hallsCinemas;
			
			return PartialView("_EditPartial", seanceViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(SeanceViewModel seanceViewModel)
		{
			if (seanceViewModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (!ModelState.IsValid)
			{
				return PartialView("_EditPartial", seanceViewModel);
			}

			SeanceDomainModel seanceDomainModel = Mapper.Map<SeanceViewModel, SeanceDomainModel>(seanceViewModel);
			_seanceService.Update(seanceDomainModel);

			var url = Url.Action("List", "Seance", routeValues: new { filmId = seanceViewModel.FilmId });
			return Json(new { success = true, url = url, replaceTarget = "#SeanceList" });
		}

		public ActionResult Details(long? seanceId)
		{
			if (seanceId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			SeanceDomainModel seanceDomianModel = _seanceService.ReadById(seanceId);
			SeanceViewModel seanceViewModel = Mapper.Map<SeanceDomainModel, SeanceViewModel>(seanceDomianModel);

			if (seanceViewModel == null)
			{
				return HttpNotFound();
			}

			return PartialView("_DetailsPartial", seanceViewModel);
		}

		public ActionResult Delete(long? seanceId)
		{
			if (seanceId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			SeanceDomainModel seanceDomainModel = _seanceService.ReadById(seanceId);
			SeanceViewModel seanceViewModel = Mapper.Map<SeanceDomainModel, SeanceViewModel>(seanceDomainModel);

			if (seanceViewModel == null)
			{
				return HttpNotFound();
			}

			return PartialView("_DeletePartial", seanceViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(long seanceId)
		{
			SeanceDomainModel seanceDomainModel = _seanceService.ReadById(seanceId);
			
			if (seanceDomainModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			
			SeanceViewModel seanceViewModel = Mapper.Map<SeanceDomainModel, SeanceViewModel>(seanceDomainModel);
			
			if (!ModelState.IsValid)
			{
				return PartialView("_DeletePartial", seanceViewModel);
			}

			_seanceService.Delete(seanceDomainModel);

			var url = Url.Action("List", "Seance", routeValues: new { filmId = seanceViewModel.FilmId });
			return Json(new { success = true, url = url, replaceTarget = "#SeanceList" });
		}

		#region Helpers
		private IEnumerable<SelectListItem> GetHallsCinemas()
		{
			IEnumerable<SelectListItem> hallsCinemas = _hallService
				.Read()
				.Select(x => new SelectListItem()
				{
					Value = x.Id.ToString(),
					Text = x.Cinema.Name + " " + x.Name
				});

			return hallsCinemas;
		} 
		#endregion
	}
}
