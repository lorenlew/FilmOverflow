using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
    public class Film : Entity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string TrailerPath { get; set; }

        public string Duration { get; set; }

        public virtual ICollection<Review> Tickets { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
    }
}
