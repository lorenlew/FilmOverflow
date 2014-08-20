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
	public class HallController : Controller
	{
		private readonly IHallService _hallService;

		public HallController(IHallService hallService)
		{
			_hallService = hallService;
		}


		public ActionResult Index()
		{
			IEnumerable<HallDomainModel> hallDomainModel = _hallService.Read();
			IEnumerable<HallViewModel> hallViewModel =
				(Mapper.Map<IEnumerable<HallDomainModel>, IEnumerable<HallViewModel>>(hallDomainModel)).ToList();
			return View(hallViewModel);
		}

		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			HallDomainModel hallDomainModel = _hallService.ReadById(id);
			HallViewModel hallViewModel = Mapper.Map<HallDomainModel, HallViewModel>(hallDomainModel);
			if (hallViewModel == null)
			{
				return HttpNotFound();
			}
			return View(hallViewModel);
		}

		public ActionResult Create(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ViewBag.CinemaId = id;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,RowAmount,ColumnAmount,CinemaId")] HallViewModel hallViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(hallViewModel);
			}
			HallDomainModel hallDomainModel = Mapper.Map<HallViewModel, HallDomainModel>(hallViewModel);
			_hallService.Add(hallDomainModel);
			return RedirectToAction("Details", "Cinema", new { id = hallDomainModel.CinemaId });
		}

		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			HallDomainModel hallDomainModel = _hallService.ReadById(id);
			HallViewModel hallViewModel = Mapper.Map<HallDomainModel, HallViewModel>(hallDomainModel);
			if (hallViewModel == null)
			{
				return HttpNotFound();
			}
			return View(hallViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,RowAmount,ColumnAmoun,CinemaId")] HallViewModel hallViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(hallViewModel);
			}
			HallDomainModel hallDomainModel = Mapper.Map<HallViewModel, HallDomainModel>(hallViewModel);
			_hallService.Update(hallDomainModel);
			return RedirectToAction("Index");
		}

		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			HallDomainModel hallDomainModel = _hallService.ReadById(id);
			HallViewModel hallViewModel = Mapper.Map<HallDomainModel, HallViewModel>(hallDomainModel);
			if (hallViewModel == null)
			{
				return HttpNotFound();
			}
			return View(hallViewModel);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			HallDomainModel hallDomainModel = _hallService.ReadById(id);
			_hallService.Delete(hallDomainModel);
			return RedirectToAction("Index");
		}

	}
}
