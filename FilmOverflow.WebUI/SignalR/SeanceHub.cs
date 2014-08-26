using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Ninject;

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

		public void GetReservedSeatsForSeance(long id, bool isInit)
		{

			IEnumerable<ReservedSeatDomainModel> reservedSeatDomainModel =
				_seanceService.ReadById(id).ReservedSeats;

			var reservedSeats = (from seat in reservedSeatDomainModel
								 select new
								 {
									 seat.Id,
									 seat.RowNumber,
									 seat.ColumnNumber,
									 seat.ReservationTime
								 }).ToList();
			string jsonData = JsonConvert.SerializeObject(reservedSeats);
			if (isInit)
			{
				Clients.All.notify(jsonData);
			}
			else
			{
				Clients.Others.notify(jsonData);
			}
		}
	}
}