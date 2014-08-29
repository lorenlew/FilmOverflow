using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;
using FilmOverflow.WebUI.ViewModels.Seance;

namespace FilmOverflow.WebUI.Orhestrators
{
	public class HomeOrhestrator
	{
		private readonly ICinemaService _cinemaService;
		private readonly IFilmService _filmService;
		private readonly ISeanceService _seanceService;

		public HomeOrhestrator(ICinemaService cinemaService, IFilmService filmService, ISeanceService seanceService)
		{
			_cinemaService = cinemaService;
			_filmService = filmService;
			_seanceService = seanceService;
		}

		public IEnumerable<string> GetAllSeancesDates()
		{
			var todayString = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
			IEnumerable<string> dates = _seanceService
				.Read()
				.Select(Mapper.Map<SeanceDomainModel, SeanceViewModel>)
				.Where(x => x.Date.CompareTo(todayString) >= 0)
				.Select(x => x.Date)				
				.Distinct()
				.OrderBy(x => x);

			return dates;
		}

		public IEnumerable<CinemaRowViewModel> GetCinemaSchedule(string date)
		{
			IEnumerable<CinemaRowViewModel> cinemaRows = _cinemaService
				.Read()
				.Select(x =>
				{
					var cinema = Mapper.Map<CinemaDomainModel, CinemaViewModel>(x);

					var list = new List<SeanceViewModel>();
					foreach (var hall in cinema.Halls)
					{
						list.AddRange(hall.Seances);
					}

					var seances = list.AsEnumerable();
					if (date != null)
					{
						seances = seances.Where(h => h.Date == date);
					}
					var films = seances
						.Select(s => s.FilmId)
						.Distinct()
						.Select(filmId =>
						{
							var domainFilm = _filmService.ReadById(filmId);
							var viewFilm = Mapper.Map<FilmDomainModel, FilmViewModel>(domainFilm);
							viewFilm.Seances = viewFilm.Seances.Where(s => s.Hall.CinemaId == cinema.Id).ToList();
							if (date != null)
							{
								viewFilm.Seances = viewFilm.Seances.Where(s => s.Date == date).ToList();
							}
							return viewFilm;
						});

					//.Select(filmId => Mapper.Map<FilmDomainModel, FilmViewModel>(_filmService.ReadById(filmId)));
					var cinemaRowViewModel = new CinemaRowViewModel()
					{
						Cinema = cinema,
						Films = films
					};

					return cinemaRowViewModel;
				});

			return cinemaRows;
		}
	}
}