using System;
using System.Collections.Generic;
using System.Linq;
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
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Ticket ticket = Mapper.Map<TicketDomainModel, Ticket>(entity);
			Uow.GetRepository<Ticket>().Add(ticket);
			Uow.Save();
		}

		public IEnumerable<TicketDomainModel> Read()
		{
			IQueryable<Ticket> tickets = Uow.GetRepository<Ticket>().Read();
			IEnumerable<TicketDomainModel> ticketsDomain = Mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDomainModel>>(tickets);
			return ticketsDomain;
		}

		public TicketDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Ticket ticket = Uow.GetRepository<Ticket>().ReadById(id);
			TicketDomainModel ticketDomain = Mapper.Map<Ticket, TicketDomainModel>(ticket);
			return ticketDomain;
		}

		public void Update(TicketDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Ticket ticket = Mapper.Map<TicketDomainModel, Ticket>(entity);
			Uow.GetRepository<Ticket>().Update(ticket);
			Uow.Save();
		}

		public void Delete(TicketDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			Ticket ticket = Mapper.Map<TicketDomainModel, Ticket>(entity);
			Uow.GetRepository<Ticket>().Delete(ticket);
			Uow.Save();
		}
	}
}
