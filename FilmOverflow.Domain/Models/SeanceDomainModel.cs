using System;
using System.Collections.Generic;

namespace FilmOverflow.Domain.Models
{
	public class SeanceDomainModel : EntityDomainModel
	{
		public DateTime Date { get; set; }
		
		public DateTime Time { get; set; }
		
		public decimal Price { get; set; }

		public long FilmId { get; set; }

		public long CinemaId { get; set; }

		public ICollection<TicketDomainModel> Tickets { get; set; }
	}
}
