using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class SeanceService : BaseService, ISeanceService
	{
		public SeanceService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(SeanceDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Seance seance = Mapper.Map<SeanceDomainModel, Seance>(entity);
			Uow.GetRepository<Seance>().Add(seance);
			Uow.Save();
		}

		public IEnumerable<SeanceDomainModel> Read()
		{
			IQueryable<Seance> seances = Uow.GetRepository<Seance>().Read();
			IEnumerable<SeanceDomainModel> seancesDomain = Mapper.Map<IEnumerable<Seance>,
				IEnumerable<SeanceDomainModel>>(seances);
			return seancesDomain;
		}

		public SeanceDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Seance seance = Uow.GetRepository<Seance>().ReadById(id);
			SeanceDomainModel seanceDomain = Mapper.Map<Seance, SeanceDomainModel>(seance);
			return seanceDomain;
		}

		public void Update(SeanceDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			var seance = Mapper.Map<SeanceDomainModel, Seance>(entity);
			Uow.GetRepository<Seance>().Update(seance);
			Uow.Save();
		}

		public void Delete(SeanceDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Seance seance = Mapper.Map<SeanceDomainModel, Seance>(entity);
			Uow.GetRepository<Seance>().Delete(seance);
			Uow.Save();
		}
	}
}
