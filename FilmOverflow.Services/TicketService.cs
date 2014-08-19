using System;
using System.Collections.Generic;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class TicketService : BaseService, ITicketService
	{
		public TicketService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(TicketDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var ticket = Mapper.Map<TicketDomainModel, Ticket>(entity);
			Uow.GetRepository<Ticket>().Add(ticket);
			Uow.Save();
		}

		public IEnumerable<TicketDomainModel> Read()
		{
			var tickets = Uow.GetRepository<Ticket>().Read();
			var ticketsDomain = Mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDomainModel>>(tickets);
			return ticketsDomain;
		}

		public TicketDomainModel ReadById(object id)
		{
			var ticket = Uow.GetRepository<Ticket>().ReadById(id);
			var ticketDomain = Mapper.Map<Ticket, TicketDomainModel>(ticket);
			return ticketDomain;
		}

		public void Update(TicketDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var ticket = Mapper.Map<TicketDomainModel, Ticket>(entity);
			Uow.GetRepository<Ticket>().Update(ticket);
			Uow.Save();
		}

		public void Delete(TicketDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var ticket = Mapper.Map<TicketDomainModel, Ticket>(entity);
			Uow.GetRepository<Ticket>().Delete(ticket);
			Uow.Save();
		}

		public void DisableValidationOnSave()
		{
			Uow.DisableValidationOnSave();
		}
	}
}
