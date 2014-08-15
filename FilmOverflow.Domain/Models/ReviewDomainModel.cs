using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
    public class ReviewDomainModel : EntityDomainModel
    {
        public string ApplicationUserId { get; set; }

        public string FilmId { get; set; }

        public int Rate { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
