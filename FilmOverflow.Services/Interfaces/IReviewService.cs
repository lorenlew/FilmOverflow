using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
    public interface IReviewService : IService
    {
        void Add(ReviewDomainModel entity);

        IEnumerable<ReviewDomainModel> Read();

        ReviewDomainModel ReadById(int id);

        void Update(ReviewDomainModel entity);

        void Delete(ReviewDomainModel entity);

        void Save();

        void DisableValidationOnSave();

    }
}
