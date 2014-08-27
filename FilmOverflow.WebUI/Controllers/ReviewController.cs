using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IReviewService _reviewService;

		public ReviewController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		public ActionResult Index()
		{
			IEnumerable<ReviewDomainModel> reviewDomainModel = _reviewService.Read();
			IEnumerable<ReviewViewModel> reviewViewModel =
				(Mapper.Map<IEnumerable<ReviewDomainModel>, IEnumerable<ReviewViewModel>>(reviewDomainModel)).ToList();
			return View(reviewViewModel);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ReviewViewModel reviewViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(reviewViewModel);
			}
			ReviewDomainModel reviewDomainModel = Mapper.Map<ReviewViewModel, ReviewDomainModel>(reviewViewModel);
			_reviewService.Add(reviewDomainModel);
			return RedirectToAction("Index");
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
			return View(reviewViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ReviewViewModel reviewViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(reviewViewModel);
			}
			ReviewDomainModel reviewDomainModel = Mapper.Map<ReviewViewModel, ReviewDomainModel>(reviewViewModel);
			_reviewService.Update(reviewDomainModel);
			return RedirectToAction("Index");
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
			return RedirectToAction("Index");
		}
	}
}
