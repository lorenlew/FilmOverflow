using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
    public class Cinema : Entity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
