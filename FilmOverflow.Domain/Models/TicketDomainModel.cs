using System;

namespace FilmOverflow.Domain.Models
{
    public class TicketDomainModel : EntityDomainModel
    {
        public string ApplicationUserId { get; set; }

        public long SeanceId { get; set; }

        public int SeatNumber { get; set; }

        public long PaymentMethodId { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
