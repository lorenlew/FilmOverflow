using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
