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
	public class HallService : BaseService, IHallService
	{
		public HallService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(HallDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Hall hall = Mapper.Map<HallDomainModel, Hall>(entity);
			Uow.GetRepository<Hall>().Add(hall);
			Uow.Save();
		}

		public IEnumerable<HallDomainModel> Read()
		{
			IQueryable<Hall> halls = Uow.GetRepository<Hall>().Read();
			IEnumerable<HallDomainModel> hallsDomain = Mapper.Map<IEnumerable<Hall>,
				IEnumerable<HallDomainModel>>(halls);
			return hallsDomain;
		}

		public HallDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Hall hall = Uow.GetRepository<Hall>().ReadById(id);
			HallDomainModel hallDomain = Mapper.Map<Hall, HallDomainModel>(hall);
			return hallDomain;
		}

		public void Update(HallDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Hall hall = Mapper.Map<HallDomainModel, Hall>(entity);
			Uow.GetRepository<Hall>().Update(hall);
			Uow.Save();
		}

		public void Delete(HallDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Hall hall = Mapper.Map<HallDomainModel, Hall>(entity);
			Uow.GetRepository<Hall>().Delete(hall);
			Uow.Save();
		}
	}
}
