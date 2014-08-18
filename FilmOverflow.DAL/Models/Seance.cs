using System;
using System.Collections.Generic;

namespace FilmOverflow.DAL.Models
{
    public class Seance : Entity
    {
        public long FilmId { get; set; }

        public long CinemaId { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public DateTime SeanceDate { get; set; }

        public decimal Price { get; set; }

    }
}
