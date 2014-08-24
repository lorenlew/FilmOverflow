using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using FilmOverflow.WebUI.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace FilmOverflow.WebUI.SignalR
{
	[HubName("seanceService")]
	public class SeanceHub : Hub
	{
		private readonly ISeanceService _seanceService;

		public SeanceHub(ISeanceService seanceService)
		{
			_seanceService = seanceService;
		}
		public string GetReservedSeatsForSeance(long id)
		{
			var reservedSeatDomainModel =
				_seanceService.ReadById(id).ReservedSeats.ToList();
			var reservedSeats = from seat in reservedSeatDomainModel
								select new
								{
									seat.RowNumber,
									seat.ColumnNumber,
									seat.ReservationTime
								};
			var jsonData = JsonConvert.SerializeObject(reservedSeats);
			return jsonData;
		}

	}
}