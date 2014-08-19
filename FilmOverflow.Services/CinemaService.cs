using System;
using System.Collections.Generic;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class CinemaService : BaseService, ICinemaService
	{
		public CinemaService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(CinemaDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var cinema = Mapper.Map<CinemaDomainModel, Cinema>(entity);
			Uow.GetRepository<Cinema>().Add(cinema);
			Uow.Save();
		}

		public IEnumerable<CinemaDomainModel> Read()
		{
			var cinemas = Uow.GetRepository<Cinema>().Read();
			var cinemasDomain = Mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaDomainModel>>(cinemas);
			return cinemasDomain;
		}

		public CinemaDomainModel ReadById(object id)
		{
			var cinema = Uow.GetRepository<Cinema>().ReadById(id);
			var cinemaDomain = Mapper.Map<Cinema, CinemaDomainModel>(cinema);
			return cinemaDomain;
		}

		public void Update(CinemaDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var cinema = Mapper.Map<CinemaDomainModel, Cinema>(entity);
			Uow.GetRepository<Cinema>().Update(cinema);
			Uow.Save();
		}

		public void Delete(CinemaDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var cinema = Mapper.Map<CinemaDomainModel, Cinema>(entity);
			Uow.GetRepository<Cinema>().Delete(cinema);
			Uow.Save();
		}

		public void DisableValidationOnSave()
		{
			Uow.DisableValidationOnSave();
		}
	}
}
