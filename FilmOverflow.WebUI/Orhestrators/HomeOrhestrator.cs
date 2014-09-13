using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;
using FilmOverflow.WebUI.ViewModels.Seance;
using Ninject.Infrastructure.Language;

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
			IEnumerable<string> dates = _seanceService
				.Read()
				.Where(x => x.Date >= DateTime.Today)
				.Select(Mapper.Map<SeanceDomainModel, SeanceViewModel>)
				.Select(x => x.Date)
				.Distinct()
				.OrderBy(x => x);

			return dates;
		}

		public IEnumerable<CinemaRowViewModel> GetCinemaSchedule(string date)
		{
			IEnumerable<CinemaRowViewModel> cinemaRows = _cinemaService
				.Read()
				.Select(c =>
			{
				var cinema = Mapper.Map<CinemaDomainModel, CinemaViewModel>(c);

				var films = _seanceService
					.Read()
					.Select(Mapper.Map<SeanceDomainModel, SeanceViewModel>)
					.Where(s => s.Hall.CinemaId == cinema.Id && s.Date == date)
					.GroupBy(s => s.FilmId)
					.Select(g =>
					{
						var domainFilm = _filmService.ReadById(g.Key);
						var viewFilm = Mapper.Map<FilmDomainModel, FilmViewModel>(domainFilm);
						viewFilm.Seances = g.ToList();

						return viewFilm;
					});

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