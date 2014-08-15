using System;
using System.Collections.Generic;
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
            if (entity == null) throw new ArgumentNullException("entity");
            var seance = Mapper.Map<SeanceDomainModel, Seance>(entity);
            Uow.GetRepository<Seance>().Add(seance);
        }

        public IEnumerable<SeanceDomainModel> Read()
        {
            var seances = Uow.GetRepository<Seance>().Read();
            var seancesDomain = Mapper.Map<IEnumerable<Seance>, IEnumerable<SeanceDomainModel>>(seances);
            return seancesDomain;
        }

        public SeanceDomainModel ReadById(int id)
        {
            var seance = Uow.GetRepository<Seance>().ReadById(id);
            var seanceDomain = Mapper.Map<Seance, SeanceDomainModel>(seance);
            return seanceDomain;
        }

        public void Update(SeanceDomainModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var seance = Mapper.Map<SeanceDomainModel, Seance>(entity);
            Uow.GetRepository<Seance>().Update(seance);
        }

        public void Delete(SeanceDomainModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var seance = Mapper.Map<SeanceDomainModel, Seance>(entity);
            Uow.GetRepository<Seance>().Delete(seance);
        }

        public void Save()
        {
            Uow.Save();
        }

        public void DisableValidationOnSave()
        {
            Uow.DisableValidationOnSave();
        }
    }
}
