using System;
using System.Collections.Generic;
using System.Linq;
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

	    public SeanceController(ISeanceService seanceService)
	    {
			if (seanceService == null)
			{
				throw new ArgumentNullException("seanceService");
			}
			_seanceService = seanceService;
	    }

        public ActionResult Index()
        {
			var seancesDomainModel = _seanceService.Read();
			var seancesViewModel = Mapper.Map<IEnumerable<SeanceDomainModel>, IEnumerable<SeanceViewModel>>(seancesDomainModel);

			return PartialView("_IndexPartial", seancesViewModel);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
