using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class ReservedSeatService : BaseService, IReservedSeatService
	{
		public ReservedSeatService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(ReservedSeatDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			ReservedSeat reservedSeat = Mapper.Map<ReservedSeatDomainModel, ReservedSeat>(entity);
			Uow.GetRepository<ReservedSeat>().Add(reservedSeat);
			Uow.Save();
		}

		public IEnumerable<ReservedSeatDomainModel> Read()
		{
			IQueryable<ReservedSeat> reservedSeats = Uow.GetRepository<ReservedSeat>().Read();
			IEnumerable<ReservedSeatDomainModel> reservedSeatsDomain = Mapper.Map<IEnumerable<ReservedSeat>,
				IEnumerable<ReservedSeatDomainModel>>(reservedSeats);
			return reservedSeatsDomain;
		}

		public ReservedSeatDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			ReservedSeat reservedSeat = Uow.GetRepository<ReservedSeat>().ReadById(id);
			ReservedSeatDomainModel reservedSeatDomain = Mapper.Map<ReservedSeat,
				ReservedSeatDomainModel>(reservedSeat);
			return reservedSeatDomain;
		}

		public void Update(ReservedSeatDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			ReservedSeat reservedSeat = Mapper.Map<ReservedSeatDomainModel, ReservedSeat>(entity);
			Uow.GetRepository<ReservedSeat>().Update(reservedSeat);
			Uow.Save();
		}

		public void Delete(ReservedSeatDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			ReservedSeat reservedSeat = Mapper.Map<ReservedSeatDomainModel, ReservedSeat>(entity);
			Uow.GetRepository<ReservedSeat>().Delete(reservedSeat);
			Uow.Save();
		}

		public void ExemptExpiredSeats(long currentSeanceId)
		{
			Seance currentSeance = Uow.GetRepository<Seance>().ReadById(currentSeanceId);
			if (currentSeance == null)
			{
				return;
			}
			List<ReservedSeat> seatsToDelete = (from seat in currentSeance.ReservedSeats
												where DateTime.Compare(DateTime.Now, seat.ReservationTime.AddMinutes(10)) > 0
													&& !seat.IsSold
												select seat).ToList();
			IEnumerable<ReservedSeatDomainModel> seatsToDeleteDomainModel = Mapper.Map<IEnumerable<ReservedSeat>,
				IEnumerable<ReservedSeatDomainModel>>(seatsToDelete);
			foreach (var seat in seatsToDeleteDomainModel)
			{
				Delete(seat);
			}
		}
	}
}
