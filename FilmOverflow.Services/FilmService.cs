using System;
using System.Collections.Generic;
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
            if (entity == null) throw new ArgumentNullException("entity");
            var film = Mapper.Map<FilmDomainModel, Film>(entity);
            Uow.GetRepository<Film>().Add(film);
            Uow.Save();
        }

        public IEnumerable<FilmDomainModel> Read()
        {
            var films = Uow.GetRepository<Film>().Read();
            var filmsDomain = Mapper.Map<IEnumerable<Film>, IEnumerable<FilmDomainModel>>(films);
            return filmsDomain;
        }

        public FilmDomainModel ReadById(object id)
        {
            var film = Uow.GetRepository<Film>().ReadById(id);
            var filmDomain = Mapper.Map<Film, FilmDomainModel>(film);
            return filmDomain;
        }

        public void Update(FilmDomainModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var film = Mapper.Map<FilmDomainModel, Film>(entity);
            Uow.GetRepository<Film>().Update(film);
            Uow.Save();
        }

        public void Delete(FilmDomainModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var film = Mapper.Map<FilmDomainModel, Film>(entity);
            Uow.GetRepository<Film>().Delete(film);
            Uow.Save();
        }

        public void DisableValidationOnSave()
        {
            Uow.DisableValidationOnSave();
        }
    }
}
