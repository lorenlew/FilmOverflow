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
	public class FilmService : BaseService, IFilmService
	{
		public FilmService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(FilmDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Film film = Mapper.Map<FilmDomainModel, Film>(entity);
			Uow.GetRepository<Film>().Add(film);
			Uow.Save();
		}

		public IEnumerable<FilmDomainModel> Read()
		{
			IQueryable<Film> films = Uow.GetRepository<Film>().Read();
			IEnumerable<FilmDomainModel> filmsDomain = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmDomainModel>>(films);
			return filmsDomain;
		}

		public FilmDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Film film = Uow.GetRepository<Film>().ReadById(id);
			FilmDomainModel filmDomain = Mapper.Map<Film, FilmDomainModel>(film);
			return filmDomain;
		}

		public void Update(FilmDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Film film = Mapper.Map<FilmDomainModel, Film>(entity);
			Uow.GetRepository<Film>().Update(film);
			Uow.Save();
		}

		public void Delete(FilmDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Film film = Mapper.Map<FilmDomainModel, Film>(entity);
			Uow.GetRepository<Film>().Delete(film);
			Uow.Save();
		}
	}
}
