using System;

namespace FilmOverflow.Domain.Models
{
	public class TicketDomainModel : EntityDomainModel
	{
		public DateTime PaymentDate { get; set; }

		public long SeatId { get; set; }

		public string ApplicationUserId { get; set; }

		public long SeanceId { get; set; }

		public long PaymentMethodId { get; set; }
	}
}
