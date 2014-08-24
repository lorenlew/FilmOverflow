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
		public HomeController(IUserManagerService userManagerService)
		{
			_userManagerService = userManagerService;
		}

		public ActionResult Index()
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