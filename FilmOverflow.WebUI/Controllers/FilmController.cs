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
            if (filmService == null) throw new ArgumentNullException("filmService");
            _filmService = filmService;
        }

        public ActionResult Index()
        {
            var films = _filmService.Read();
            var filmsViewModel = Mapper.Map<IEnumerable<FilmDomainModel>, IEnumerable<FilmViewModel>>(films);

            return View("Index", filmsViewModel);
        }

        public ActionResult Create()
        {
            return PartialView("_CreatePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (!ModelState.IsValid) return PartialView("_CreatePartial", model);

            //var image = Convert.FromBase64String(model.ImageEncodedBase64);
            
            //var extension = Path.GetExtension((model.Image.FileName));
            //if (extension == null)
            //{
            //    return PartialView("_CreatePartial", model);
            //}
            
            //var fileName = Guid.NewGuid() + "." + extension.Substring(1);
            //var virtualPath = "/Content/Images/Film-Images/" + fileName;
            //var physicalPath = HttpContext.Server.MapPath(virtualPath);
            //model.ImagePath = virtualPath;

            //var film = Mapper.Map<FilmViewModel, FilmDomainModel>(model);
            //_filmService.Add(film);
            //model.Image.SaveAs(physicalPath);

            var url = Url.Action("Index", "Film");
            return Json(new { success = true, url = url, replaceTarget = "#FilmManagement" });
        }

        public ActionResult Edit(long filmId)
        {
            return PartialView();
        }

        public ActionResult Details(long filmId)
        {
            return PartialView();
        }        
    }
}