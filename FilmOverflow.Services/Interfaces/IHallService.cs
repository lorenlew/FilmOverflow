using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
	public interface IHallService : IService
	{
		void Add(HallDomainModel entity);

		IEnumerable<HallDomainModel> Read();

		HallDomainModel ReadById(object id);

		void Update(HallDomainModel entity);

		void Delete(HallDomainModel entity);
	}
}
