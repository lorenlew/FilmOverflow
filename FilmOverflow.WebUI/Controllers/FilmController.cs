using System;
using System.Collections.Generic;
using System.IO;
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
	public class FilmController : Controller
	{
		private readonly IFilmService _filmService;

		public FilmController(IFilmService filmService)
		{
			if (filmService == null)
			{
				throw new ArgumentNullException("filmService");
			}
			_filmService = filmService;
		}

		public ActionResult Index()
		{
			var filmsDomainModel = _filmService.Read();
			var filmsViewModel = Mapper.Map<IEnumerable<FilmDomainModel>, IEnumerable<FilmViewModel>>(filmsDomainModel);

			return View("Index", filmsViewModel);
		}

		public ActionResult Create()
		{
			return View("Create");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(FilmViewModel filmViewModel)
		{
			if (filmViewModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (!ModelState.IsValid) return View("Create", filmViewModel);

			var extension = Path.GetExtension((filmViewModel.Image.FileName));
			if (extension == null)
			{
				return View("Create", filmViewModel);
			}

			var fileName = Guid.NewGuid() + "." + extension.Substring(1);
			var virtualPath = "/Content/Images/Film-Images/" + fileName;
			var physicalPath = HttpContext.Server.MapPath(virtualPath);
			filmViewModel.ImagePath = virtualPath;

			var filmDomainModel = Mapper.Map<FilmViewModel, FilmDomainModel>(filmViewModel);
			_filmService.Add(filmDomainModel);
			filmViewModel.Image.SaveAs(physicalPath);

			return RedirectToAction("Index", "Film");
		}

		public ActionResult Edit(long filmId)
		{
			var filmDomainModel = _filmService.ReadById(filmId);
			var filmViewModel = Mapper.Map<FilmDomainModel, FilmViewModel>(filmDomainModel);

			return View("Edit", filmViewModel);
		}

		public ActionResult Details(long filmId)
		{
			return View();
		}
	}
}