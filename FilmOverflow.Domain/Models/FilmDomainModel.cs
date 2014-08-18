using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace FilmOverflow.Domain.Models
{
    public class FilmDomainModel : EntityDomainModel
    {
        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [NotMapped]
        public string ImageEncodedBase64 { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [StringLength(60)]
        public string Duration { get; set; }

        public ICollection<ReviewDomainModel> Reviews { get; set; }

        public ICollection<SeanceDomainModel> Seances { get; set; }
    }
}
