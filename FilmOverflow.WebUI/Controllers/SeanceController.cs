﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.Attributes;
using FilmOverflow.WebUI.ViewModels;
using FilmOverflow.WebUI.ViewModels.Seance;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace FilmOverflow.WebUI.Controllers
{
	public class SeanceController : Controller
	{
		private readonly ISeanceService _seanceService;
		private readonly IHallService _hallService;
		private readonly IReservedSeatService _reservedSeatService;

		public SeanceController(ISeanceService seanceService, IHallService hallService, IReservedSeatService reservedSeatService)
		{
			_seanceService = seanceService;
			_hallService = hallService;
			_reservedSeatService = reservedSeatService;
		}

		[Authorize(Roles = "Administrator, Moderator")]
		public ActionResult Index(long filmId)
		{
			ViewBag.FilmId = filmId;

			return View("Index");
		}

		[Authorize(Roles = "Administrator, Moderator")]
		public ActionResult List(long filmId)
		{
			var seancesDomainModel = _seanceService
				.Read()
				.Where(x => x.FilmId == filmId);
			var seancesViewModel = Mapper.Map<IEnumerable<SeanceDomainModel>, IEnumerable<SeanceViewModel>>(seancesDomainModel);
			ViewBag.FilmId = filmId;

			return PartialView("_ListPartial", seancesViewModel);
		}

		[Authorize(Roles = "Administrator, Moderator")]
		public ActionResult Create(long filmId)
		{
			IEnumerable<SelectListItem> hallsCinemas = GetHallsCinemas();
			ViewBag.HallsCinemas = hallsCinemas;
			ViewBag.FilmId = filmId;

			return PartialView("_CreatePartial");
		}

		[Authorize(Roles = "Administrator, Moderator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateSeanceViewModel createSeanceViewModel)
		{
			if (createSeanceViewModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (!ModelState.IsValid)
			{
				ViewBag.HallsCinemas = GetHallsCinemas();
				return PartialView("_CreatePartial", createSeanceViewModel);
			}

			if (createSeanceViewModel.IsMultipleDateSelect)
			{
				var startDate = DateTime.ParseExact(createSeanceViewModel.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				var endDate = DateTime.ParseExact(createSeanceViewModel.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				for (var date = startDate; date <= endDate; date = date.AddDays(1.0))
				{
					SeanceViewModel baseSeanceViewModel = createSeanceViewModel;
					SeanceDomainModel newSeanceDomainModel = Mapper.Map<SeanceViewModel, SeanceDomainModel>(baseSeanceViewModel);
					newSeanceDomainModel.Date = date;
					_seanceService.Add(newSeanceDomainModel);
				}
			}
			else
			{
				SeanceViewModel seanceViewModel = createSeanceViewModel;
				SeanceDomainModel seanceDomainModel = Mapper.Map<SeanceViewModel, SeanceDomainModel>(seanceViewModel);
				_seanceService.Add(seanceDomainModel);
			}
			
			var url = Url.Action("List", "Seance", routeValues: new { filmId = createSeanceViewModel.FilmId });
			return Json(new { success = true, url = url, replaceTarget = "#SeanceList" });
		}

		[Authorize(Roles = "Administrator, Moderator")]
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

		[Authorize(Roles = "Administrator, Moderator")]
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

		public ActionResult Details(long? seanceId, bool isCommon)
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
			return PartialView(isCommon ? "_Details" : "_DetailsPartial", seanceViewModel);
		}

		[Authorize(Roles = "Administrator, Moderator")]
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

		[Authorize(Roles = "Administrator, Moderator")]
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

		[Authorize]
		public string GetSeanceSeats(long seanceId)
		{
			SeanceDomainModel currentSeance = _seanceService.ReadById(seanceId);
			if (currentSeance == null)
			{
				HttpNotFound();
			}
			else
			{
				var seanceHall = currentSeance.Hall;
				if (seanceHall == null)
				{
					HttpNotFound();
				}
				else
				{
					string jsonData = JsonConvert.SerializeObject(seanceHall.Seats);
					return jsonData;
				}
			}
			return null;
		}

		[HttpPost]
		[Authorize]
		public ActionResult ToogleReservationStatus([FromJson] SeatViewModel seatViewModel, long seanceId)
		{
			SeanceDomainModel currentSeance = _seanceService.ReadById(seanceId);
			string currentUserId = User.Identity.GetUserId();

			if (currentSeance == null || seatViewModel == null)
			{
				HttpNotFound();
			}
			else
			{
				var reservedSeat = (from seat in currentSeance.ReservedSeats
									where seat.RowNumber == seatViewModel.RowNumber
									  && seat.ColumnNumber == seatViewModel.ColumnNumber
									select seat).SingleOrDefault();
				if (reservedSeat == null)
				{
					ReservedSeatDomainModel currentReservedSeat = new ReservedSeatDomainModel
					{
						RowNumber = seatViewModel.RowNumber,
						ColumnNumber = seatViewModel.ColumnNumber,
						ReservationTime = DateTime.Now,
						SeanceId = seanceId,
						ApplicationUserId = currentUserId
					};
					_reservedSeatService.Add(currentReservedSeat);
				}
				else
				{
					if (reservedSeat.ApplicationUserId != currentUserId)
					{
						return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
					}
					_reservedSeatService.Delete(reservedSeat);
				}
			}
			return new HttpStatusCodeResult(HttpStatusCode.OK); ;
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
