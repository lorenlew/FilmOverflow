using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.Orhestrators
{
	public class HomeOrhestrator
	{
		private readonly ICinemaService _cinemaService;
		private readonly IFilmService _filmService;

		public HomeOrhestrator(ICinemaService cinemaService, IFilmService filmService)
		{
			_cinemaService = cinemaService;
			_filmService = filmService;
		}

		public IEnumerable<CinemaRowViewModel> GetBrowsePage()
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
					var seances = list;
					var films = seances
						.Select(s => s.FilmId)
						.Distinct()
						.Select(filmId => Mapper.Map<FilmDomainModel, FilmViewModel>(_filmService.ReadById(filmId)));;
					
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