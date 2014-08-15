using System;
using System.Collections.Generic;

namespace FilmOverflow.Domain.Models
{
    public class SeanceDomainModel : EntityDomainModel
    {
        public string FilmId { get; set; }

        public string CinemaId { get; set; }

        public virtual ICollection<TicketDomainModel> Tickets { get; set; }

        public DateTime SeanceDate { get; set; }

        public decimal Price { get; set; }
    }
}
