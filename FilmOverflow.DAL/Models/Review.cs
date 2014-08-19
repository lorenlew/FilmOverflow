using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
	public class Review : Entity
	{
		public int Rate { get; set; }

		[Required]
		[StringLength(1000)]
		public string Description { get; set; }

		public DateTime ReviewDate { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public virtual Film Film { get; set; }
		public long FilmId { get; set; }
	}
}
