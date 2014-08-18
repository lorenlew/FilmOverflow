﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
    public class ReviewDomainModel : EntityDomainModel
    {
        public string ApplicationUserId { get; set; }

        public long FilmId { get; set; }

        public int Rate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
