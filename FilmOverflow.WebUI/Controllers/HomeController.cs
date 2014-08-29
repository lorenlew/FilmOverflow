using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.Orhestrators;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserManagerService _userManagerService;
		private readonly IFilmService _filmService;
		private readonly HomeOrhestrator _homeOrhestrator;

		public HomeController(IUserManagerService userManagerService, IFilmService filmService, HomeOrhestrator homeOrhestrator)
		{
			_userManagerService = userManagerService;
			_filmService = filmService;
			_homeOrhestrator = homeOrhestrator;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult FilmList()
		{
			IEnumerable<FilmDomainModel> filmsDomainModel = _filmService.Read();
			IEnumerable<FilmViewModel> filmsViewModel = Mapper.Map<IEnumerable<FilmDomainModel>, IEnumerable<FilmViewModel>>(filmsDomainModel);

			return PartialView("_FilmListPartial", filmsViewModel);
		}

		[AllowAnonymous]
		public ActionResult Details(long? filmId)
		{
			if (filmId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			FilmDomainModel filmDomainModel = _filmService.ReadById(filmId);
			FilmViewModel filmViewModel = Mapper.Map<FilmDomainModel, FilmViewModel>(filmDomainModel);

			if (filmViewModel == null)
			{
				return HttpNotFound();
			}
			ViewBag.filmId = filmId;
			return View("Details", filmViewModel);
		}

		public ActionResult Browse()
		{
			var dates = _homeOrhestrator.GetAllSeancesDates();

			return View("Browse", dates);
		}
		
		public ActionResult CinemaRowList(string date)
		{
			IEnumerable<CinemaRowViewModel> cinemaRows = _homeOrhestrator.GetCinemaSchedule(date);
			
			return PartialView("_CinemaRowListPartial", cinemaRows);
		}

		public ActionResult TimeIsOut()
		{
			return View("TimeIsOut");
		}
	}
}