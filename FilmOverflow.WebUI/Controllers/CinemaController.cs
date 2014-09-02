using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.Controllers
{

	public class CinemaController : Controller
	{
		private readonly ICinemaService _cinemaService;

		public CinemaController(ICinemaService cinemaService)
		{
			_cinemaService = cinemaService;
		}

		public ActionResult Index()
		{
			IEnumerable<CinemaDomainModel> cinemaDomainModel = _cinemaService.Read();
			IEnumerable<CinemaViewModel> cinemaViewModel =
				(Mapper.Map<IEnumerable<CinemaDomainModel>, IEnumerable<CinemaViewModel>>(cinemaDomainModel)).ToList();
			return View(cinemaViewModel);
		}

		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CinemaDomainModel cinemaDomainModel = _cinemaService.ReadById(id);
			if (cinemaDomainModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			CinemaViewModel cinemaViewModel = Mapper.Map<CinemaDomainModel, CinemaViewModel>(cinemaDomainModel);
			return View(cinemaViewModel);
		}

		[Authorize(Roles = "Administrator, Moderator")]
		public ActionResult Create()
		{
			return View();
		}

		[Authorize(Roles = "Administrator, Moderator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Image,Name,Address,PhoneNumber")] CinemaViewModel cinemaViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(cinemaViewModel);
			}
			string extension = Path.GetExtension((cinemaViewModel.Image.FileName));
			if (extension == null)
			{
				return RedirectToAction("Index");
			}
			string virtualPath = FormVirtualPath(extension);
			string physicalPath = HttpContext.Server.MapPath(virtualPath);
			cinemaViewModel.ImagePath = virtualPath;

			CinemaDomainModel cinemaDomainModel = Mapper.Map<CinemaViewModel, CinemaDomainModel>(cinemaViewModel);
			_cinemaService.Add(cinemaDomainModel);
			cinemaViewModel.Image.SaveAs(physicalPath);
			return RedirectToAction("Index");
		}

		[Authorize(Roles = "Administrator, Moderator")]
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CinemaDomainModel cinemaDomainModel = _cinemaService.ReadById(id);
			if (cinemaDomainModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			CinemaViewModel cinemaViewModel = Mapper.Map<CinemaDomainModel, CinemaViewModel>(cinemaDomainModel);
			return View(cinemaViewModel);
		}

		[Authorize(Roles = "Administrator, Moderator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Address,ImagePath,PhoneNumber")] CinemaViewModel cinemaViewModel)
		{
			ModelState.Remove("Image");
			if (!ModelState.IsValid)
			{
				return View(cinemaViewModel);
			}
			CinemaDomainModel cinemaDomainModel = Mapper.Map<CinemaViewModel, CinemaDomainModel>(cinemaViewModel);
			_cinemaService.Update(cinemaDomainModel);
			return RedirectToAction("Index");
		}

		[Authorize(Roles = "Administrator, Moderator")]
		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CinemaDomainModel cinemaDomainModel = _cinemaService.ReadById(id);
			if (cinemaDomainModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			CinemaViewModel cinemaViewModel = Mapper.Map<CinemaDomainModel, CinemaViewModel>(cinemaDomainModel);
			return View(cinemaViewModel);
		}

		[Authorize(Roles = "Administrator, Moderator")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			CinemaDomainModel cinemaDomainModel = _cinemaService.ReadById(id);
			if (cinemaDomainModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			string physicalPath = HttpContext.Server.MapPath(cinemaDomainModel.ImagePath);
			System.IO.File.Delete(physicalPath);
			_cinemaService.Delete(cinemaDomainModel);
			return RedirectToAction("Index");
		}

		#region serviceMethods
		private string FormVirtualPath(string extension)
		{
			string fileName = Guid.NewGuid() + "." + extension.Substring(1);
			string virtualPath = "/Content/Images/Cinema-images/" + fileName;
			return virtualPath;
		}
		#endregion
	}
}
