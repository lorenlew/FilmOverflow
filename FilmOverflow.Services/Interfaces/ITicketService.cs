using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
	public interface ITicketService : IService
	{
		void Add(TicketDomainModel entity);

		IEnumerable<TicketDomainModel> Read();

		TicketDomainModel ReadById(object id);

		void Update(TicketDomainModel entity);

		void Delete(TicketDomainModel entity);
	}
}
