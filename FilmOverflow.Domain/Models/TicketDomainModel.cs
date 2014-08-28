using System;

namespace FilmOverflow.Domain.Models
{
	public class TicketDomainModel : EntityDomainModel
	{
		public DateTime PaymentDate { get; set; }

		public ApplicationUserDomainModel ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public SeanceDomainModel Seance { get; set; }
		public long SeanceId { get; set; }

		public PaymentMethodDomainModel PaymentMethod { get; set; }
		public long PaymentMethodId { get; set; }
		
		public long SeatId { get; set; }
	}
}
