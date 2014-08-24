using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
			IEnumerable<FilmDomainModel> filmsDomainModel = _filmService.Read();
			IEnumerable<FilmViewModel> filmsViewModel =
				Mapper.Map<IEnumerable<FilmDomainModel>, IEnumerable<FilmViewModel>>(filmsDomainModel);

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

			ModelState.Remove("Image");
			if (!ModelState.IsValid)
			{
				return View("Create", filmViewModel);
			}

			//TODO: have to put this code in a separate helper (type of FileManager)
			if (filmViewModel.Image != null)
			{
				var extension = Path.GetExtension((filmViewModel.Image.FileName));
				if (extension == null)
				{
					return View("Create", filmViewModel);
				}

				var fileName = Guid.NewGuid() + "." + extension.Substring(1);
				var virtualPath = "/Content/Images/Film-Images/" + fileName;
				var physicalPath = HttpContext.Server.MapPath(virtualPath);
				filmViewModel.ImagePath = virtualPath;
				
				filmViewModel.Image.SaveAs(physicalPath);
			}
			
			FilmDomainModel filmDomainModel = Mapper.Map<FilmViewModel, FilmDomainModel>(filmViewModel);
			_filmService.Add(filmDomainModel);
			
			return RedirectToAction("Index", "Film");
		}

		public ActionResult Edit(long? filmId)
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

			return View("Edit", filmViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(FilmViewModel filmViewModel)
		{
			if (filmViewModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			ModelState.Remove("Image");
			if (!ModelState.IsValid)
			{
				return View("Edit", filmViewModel);
			}

			if (filmViewModel.Image != null)
			{
				//TODO: have to put this code in a separate helper (type of FileManager)
				var extension = Path.GetExtension((filmViewModel.Image.FileName));
				if (extension == null)
				{
					return View("Create", filmViewModel);
				}

				var newFileName = Guid.NewGuid() + "." + extension.Substring(1);
				var newVirtualPath = "/Content/Images/Film-Images/" + newFileName;
				var newPhysicalPath = HttpContext.Server.MapPath(newVirtualPath);

				var oldVirtualPath = filmViewModel.ImagePath;
				var oldPhysicalPath = HttpContext.Server.MapPath(oldVirtualPath);

				if (System.IO.File.Exists(oldPhysicalPath))
				{
					System.IO.File.Delete(oldPhysicalPath);
				}

				filmViewModel.ImagePath = newVirtualPath;
				filmViewModel.Image.SaveAs(newPhysicalPath);
			}

			FilmDomainModel filmDomainModel = Mapper.Map<FilmViewModel, FilmDomainModel>(filmViewModel);
			_filmService.Update(filmDomainModel);

			return RedirectToAction("Index", "Film");
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

		public ActionResult Delete(long? filmId)
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

			return View("Delete", filmViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(long filmId)
		{
			FilmDomainModel filmDomainModel = _filmService.ReadById(filmId);

			if (filmDomainModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			FilmViewModel filmViewModel = Mapper.Map<FilmDomainModel, FilmViewModel>(filmDomainModel);

			if (!ModelState.IsValid)
			{
				return View("Delete", filmViewModel);
			}

			//TODO: have to put this code in a separate helper (type of FileManager)
			var virtualPath = filmDomainModel.ImagePath;
			var physicalPath = HttpContext.Server.MapPath(virtualPath);

			if (System.IO.File.Exists(physicalPath))
			{
				System.IO.File.Delete(physicalPath);
			}

			_filmService.Delete(filmDomainModel);

			return RedirectToAction("Index");
		}
	}
}