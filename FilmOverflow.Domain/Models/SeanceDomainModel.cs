using System;
using System.Collections.Generic;

namespace FilmOverflow.Domain.Models
{
    public class SeanceDomainModel : EntityDomainModel
    {
        public long FilmId { get; set; }

        public long CinemaId { get; set; }

        public ICollection<TicketDomainModel> Tickets { get; set; }

        public DateTime SeanceDate { get; set; }

        public decimal Price { get; set; }
    }
}
