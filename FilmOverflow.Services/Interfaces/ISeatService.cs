using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
	public interface ISeatService : IService
	{
		void Add(SeatDomainModel entity);

		IEnumerable<SeatDomainModel> Read();

		SeatDomainModel ReadById(object id);

		void Update(SeatDomainModel entity);

		void Delete(SeatDomainModel entity);
	}
}
