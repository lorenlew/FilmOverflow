﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmOverflow.DAL.Models
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }
    }
}
