using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
    public class Film : Entity
    {
        public string ReviewId { get; set; }

        public string SeanceId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string TrailerPath { get; set; }

        public string Duration { get; set; }
    }
}
