using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
    public interface ICinemaService : IService
    {
        void Add(CinemaDomainModel entity);

        IEnumerable<CinemaDomainModel> Read();

        CinemaDomainModel ReadById(int id);

        void Update(CinemaDomainModel entity);

        void Delete(CinemaDomainModel entity);

        void Save();

        void DisableValidationOnSave();

    }
}
