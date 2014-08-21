using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
			if (entity == null) throw new ArgumentNullException("entity");
			var hall = Mapper.Map<HallDomainModel, Hall>(entity);
			Uow.GetRepository<Hall>().Add(hall);
			Uow.Save();
		}

		public IEnumerable<HallDomainModel> Read()
		{
			var halls = Uow.GetRepository<Hall>().Read();
			var hallsDomain = Mapper.Map<IEnumerable<Hall>, IEnumerable<HallDomainModel>>(halls);
			return hallsDomain;
		}

		public HallDomainModel ReadById(object id)
		{
			var hall = Uow.GetRepository<Hall>().ReadById(id);
			var hallDomain = Mapper.Map<Hall, HallDomainModel>(hall);
			return hallDomain;
		}

		public void Update(HallDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var hall = Mapper.Map<HallDomainModel, Hall>(entity);
			Uow.GetRepository<Hall>().Update(hall);
			Uow.Save();
		}

		public void Delete(HallDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var hall = Mapper.Map<HallDomainModel, Hall>(entity);
			Uow.GetRepository<Hall>().Delete(hall);
			Uow.Save();
		}
	}
}
