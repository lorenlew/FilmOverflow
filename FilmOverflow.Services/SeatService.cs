using System;
using System.Collections.Generic;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class SeatService : BaseService, ISeatService
	{
		public SeatService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(SeatDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var seat = Mapper.Map<SeatDomainModel, Seat>(entity);
			Uow.GetRepository<Seat>().Add(seat);
			Uow.Save();
		}

		public IEnumerable<SeatDomainModel> Read()
		{
			var seats = Uow.GetRepository<Seat>().Read();
			var seatsDomain = Mapper.Map<IEnumerable<Seat>, IEnumerable<SeatDomainModel>>(seats);
			return seatsDomain;
		}

		public SeatDomainModel ReadById(object id)
		{
			var seat = Uow.GetRepository<Seat>().ReadById(id);
			var seatDomain = Mapper.Map<Seat, SeatDomainModel>(seat);
			return seatDomain;
		}

		public void Update(SeatDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var seat = Mapper.Map<SeatDomainModel, Seat>(entity);
			Uow.GetRepository<Seat>().Update(seat);
			Uow.Save();
		}

		public void Delete(SeatDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var seat = Mapper.Map<SeatDomainModel, Seat>(entity);
			Uow.GetRepository<Seat>().Delete(seat);
			Uow.Save();
		}
	}
}
