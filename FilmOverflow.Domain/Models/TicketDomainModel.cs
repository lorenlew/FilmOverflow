using System;

namespace FilmOverflow.Domain.Models
{
	public class TicketDomainModel : EntityDomainModel
	{
		public DateTime PaymentDate { get; set; }

		public string ApplicationUserId { get; set; }

		public long SeanceId { get; set; }

		public  ReservedSeatDomainModel ReservedSeat { get; set; }

		public long PaymentMethodId { get; set; }

	}
}
