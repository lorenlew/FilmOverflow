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
	public class ReviewController : Controller
	{
		private readonly IReviewService _reviewService;

		public ReviewController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		public ActionResult Index(long? filmId)
		{
			IEnumerable<ReviewDomainModel> reviewDomainModel = _reviewService.Read().OrderByDescending(model => model.ReviewDate);
			if (filmId != null)
			{
				reviewDomainModel = reviewDomainModel.Where(model => model.FilmId == filmId);
			}
			IEnumerable<ReviewViewModel> reviewViewModel =
				(Mapper.Map<IEnumerable<ReviewDomainModel>, IEnumerable<ReviewViewModel>>(reviewDomainModel)).ToList();
			return PartialView("_Index",reviewViewModel);
		}

		public ActionResult Create(long filmId)
		{
			ViewBag.filmId = filmId;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ReviewViewModel reviewViewModel, long filmId)
		{
			reviewViewModel.FilmId = filmId;
			reviewViewModel.ApplicationUserId = User.Identity.GetUserId();
			reviewViewModel.ReviewDate = DateTime.Now;
			ModelState.Remove("ReviewDate");

			if (!ModelState.IsValid)
			{
				return View(reviewViewModel);
			}
			ReviewDomainModel reviewDomainModel = Mapper.Map<ReviewViewModel, ReviewDomainModel>(reviewViewModel);
			_reviewService.Add(reviewDomainModel);
			return RedirectToAction("Details", "Home", new { filmId = filmId });
		}

		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ReviewDomainModel reviewDomainModel = _reviewService.ReadById(id);
			ReviewViewModel reviewViewModel = Mapper.Map<ReviewDomainModel, ReviewViewModel>(reviewDomainModel);
			if (reviewViewModel == null)
			{
				return HttpNotFound();
			}
			ViewBag.filmId = id;
			return View(reviewViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ReviewViewModel reviewViewModel)
		{
			reviewViewModel.ReviewDate = DateTime.Now;
			if (!ModelState.IsValid)
			{
				return View(reviewViewModel);
			}
			ReviewDomainModel reviewDomainModel = Mapper.Map<ReviewViewModel, ReviewDomainModel>(reviewViewModel);
			_reviewService.Update(reviewDomainModel);
			return RedirectToAction("Details", "Home", new { filmId = reviewViewModel.FilmId });
		}

		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ReviewDomainModel reviewDomainModel = _reviewService.ReadById(id);
			ReviewViewModel reviewViewModel = Mapper.Map<ReviewDomainModel, ReviewViewModel>(reviewDomainModel);
			if (reviewViewModel == null)
			{
				return HttpNotFound();
			}
			return View(reviewViewModel);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			ReviewDomainModel reviewDomainModel = _reviewService.ReadById(id);
			_reviewService.Delete(reviewDomainModel);
			return RedirectToAction("Details", "Home", new { filmId = id });
		}
	}
}
