using System;

namespace FilmOverflow.DAL.Models
{
	public class Ticket : Entity
	{
		public DateTime PaymentDate { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public virtual Seance Seance { get; set; }
		public long SeanceId { get; set; }

		public virtual PaymentMethod PaymentMethod { get; set; }
		public long PaymentMethodId { get; set; }

		public long SeatId { get; set; }

	}
}
