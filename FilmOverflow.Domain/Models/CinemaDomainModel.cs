using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
    public class CinemaDomainModel : EntityDomainModel
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<SeanceDomainModel> Seances { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
