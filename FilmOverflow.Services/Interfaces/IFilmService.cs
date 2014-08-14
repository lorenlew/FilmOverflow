using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
    public interface IFilmService : IService
    {
        void Add(FilmDomainModel entity);

        IEnumerable<FilmDomainModel> Read();

        FilmDomainModel ReadById(int id);

        void Update(FilmDomainModel entity);

        void Delete(FilmDomainModel entity);

        void Save();

        void DisableValidationOnSave();

    }
}
