using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
	public class Cinema : Entity
	{
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Required]
		public string ImagePath { get; set; }

		[Required]
		[StringLength(60)]
		public string Address { get; set; }

		[Required]
		[StringLength(60)]
		public string PhoneNumber { get; set; }

		public virtual ICollection<Seance> Seances { get; set; }
		
		public virtual ICollection<Hall> Halls { get; set; }
	}
}
