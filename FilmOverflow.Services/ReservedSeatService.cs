using System;
using System.Collections.Generic;
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
			if (entity == null) throw new ArgumentNullException("entity");
			var reservedSeat = Mapper.Map<ReservedSeatDomainModel, ReservedSeat>(entity);
			Uow.GetRepository<ReservedSeat>().Add(reservedSeat);
			Uow.Save();
		}

		public IEnumerable<ReservedSeatDomainModel> Read()
		{
			var reservedSeats = Uow.GetRepository<ReservedSeat>().Read();
			var reservedSeatsDomain = Mapper.Map<IEnumerable<ReservedSeat>, IEnumerable<ReservedSeatDomainModel>>(reservedSeats);
			return reservedSeatsDomain;
		}

		public ReservedSeatDomainModel ReadById(object id)
		{
			var reservedSeat = Uow.GetRepository<ReservedSeat>().ReadById(id);
			var reservedSeatDomain = Mapper.Map<ReservedSeat, ReservedSeatDomainModel>(reservedSeat);
			return reservedSeatDomain;
		}

		public void Update(ReservedSeatDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var reservedSeat = Mapper.Map<ReservedSeatDomainModel, ReservedSeat>(entity);
			Uow.GetRepository<ReservedSeat>().Update(reservedSeat);
			Uow.Save();
		}

		public void Delete(ReservedSeatDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var reservedSeat = Mapper.Map<ReservedSeatDomainModel, ReservedSeat>(entity);
			Uow.GetRepository<ReservedSeat>().Delete(reservedSeat);
			Uow.Save();
		}
	}
}
