using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace FilmOverflow.DAL.Models
{
	public class Film : Entity
	{
		[Required]
		[StringLength(60)]
		public string Title { get; set; }

		[Required]
		[StringLength(1000)]
		public string Description { get; set; }

		[Required]
		public string ImagePath { get; set; }

		[Required]
		[Range(0, 300)]
		public int Duration { get; set; }

		public virtual ICollection<Review> Reviews { get; set; }

		public virtual ICollection<Seance> Seances { get; set; }
	}
}
