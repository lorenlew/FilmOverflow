using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;
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

		private readonly IReservedSeatService _reservedSeatService;


		public SeanceHub(ISeanceService seanceService, IReservedSeatService reservedSeatService)
		{
			_seanceService = seanceService;
			_reservedSeatService = reservedSeatService;
		}

		public void GetReservedSeatsForSeance(long id, bool isInit)
		{
			SeanceDomainModel currentSeance =
				_seanceService.ReadById(id);
			if (currentSeance == null)
			{
				return;
			}
			var seatsToDelete = (from seat in currentSeance.ReservedSeats
								where DateTime.Compare(DateTime.Now, seat.ReservationTime.AddMinutes(10)) > 0
								&& !seat.IsSold
								select seat).ToList();

			foreach (var seat in seatsToDelete)
			{
				_reservedSeatService.Delete(seat);
			}
			var seatsToDeleteAfter = (from seat in currentSeance.ReservedSeats
								 where DateTime.Compare(DateTime.Now, seat.ReservationTime.AddMinutes(10)) > 0
								 && !seat.IsSold
								 select seat).ToList();

			var reservedSeats = (from seat in _seanceService.ReadById(id).ReservedSeats
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