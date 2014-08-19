using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
	public interface IReviewService : IService
	{
		void Add(ReviewDomainModel entity);

		IEnumerable<ReviewDomainModel> Read();

		ReviewDomainModel ReadById(object id);

		void Update(ReviewDomainModel entity);

		void Delete(ReviewDomainModel entity);

		void DisableValidationOnSave();

	}
}
