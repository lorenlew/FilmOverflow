using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserManagerService _userManagerService;
		private readonly IFilmService _filmService;

		public HomeController(IUserManagerService userManagerService, IFilmService filmService)
		{
			_userManagerService = userManagerService;
			_filmService = filmService;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult List()
		{
			IEnumerable<FilmDomainModel> filmsDomainModel = _filmService.Read();
			IEnumerable<FilmViewModel> filmsViewModel = Mapper.Map<IEnumerable<FilmDomainModel>, IEnumerable<FilmViewModel>>(filmsDomainModel);

			return PartialView("_ListPartial", filmsViewModel);
		}

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

			return View("Details", filmViewModel);
		}

		public ActionResult Browse()
		{
			return View();
		}

		public ActionResult UserInfo()
		{
			var currentUser = _userManagerService.FindByName(User.Identity.Name);
			var applicationUserViewModel = Mapper.Map<ApplicationUserDomainModel, ApplicationUserViewModel>(currentUser);
			return View(applicationUserViewModel);
		}
	}
}