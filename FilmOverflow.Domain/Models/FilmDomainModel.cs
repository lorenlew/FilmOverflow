using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
    public class FilmDomainModel : EntityDomainModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string TrailerPath { get; set; }

        public string Duration { get; set; }

        public virtual ICollection<ReviewDomainModel> Tickets { get; set; }

        public virtual ICollection<SeanceDomainModel> Seances { get; set; }
    }
}
