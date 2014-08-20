using System;
using System.Collections.Generic;

namespace FilmOverflow.DAL.Models
{
	public class Seance : Entity
	{
		public DateTime Date { get; set; }

		public DateTime Time { get; set; }

		public decimal Price { get; set; }

		public virtual Film Film { get; set; }
		public long FilmId { get; set; }

		public virtual Cinema Cinema { get; set; }
		public long CinemaId { get; set; }

		public virtual Hall Hall { get; set; }
		public long HallId { get; set; }

		public virtual ICollection<Ticket> Tickets { get; set; }
	}
}
