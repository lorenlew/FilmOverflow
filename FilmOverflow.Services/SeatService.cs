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
	public class SeatService : BaseService, ISeatService
	{
		public SeatService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(SeatDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Seat seat = Mapper.Map<SeatDomainModel, Seat>(entity);
			Uow.GetRepository<Seat>().Add(seat);
			Uow.Save();
		}

		public IEnumerable<SeatDomainModel> Read()
		{
			IQueryable<Seat> seats = Uow.GetRepository<Seat>().Read();
			IEnumerable<SeatDomainModel> seatsDomain = Mapper.Map<IEnumerable<Seat>,
				IEnumerable<SeatDomainModel>>(seats);
			return seatsDomain;
		}

		public SeatDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Seat seat = Uow.GetRepository<Seat>().ReadById(id);
			SeatDomainModel seatDomain = Mapper.Map<Seat, SeatDomainModel>(seat);
			return seatDomain;
		}

		public void Update(SeatDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Seat seat = Mapper.Map<SeatDomainModel, Seat>(entity);
			Uow.GetRepository<Seat>().Update(seat);
			Uow.Save();
		}

		public void Delete(SeatDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Seat seat = Mapper.Map<SeatDomainModel, Seat>(entity);
			Uow.GetRepository<Seat>().Delete(seat);
			Uow.Save();
		}
	}
}
