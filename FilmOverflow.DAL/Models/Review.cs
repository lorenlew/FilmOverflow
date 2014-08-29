using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
	public class Review : Entity
	{
		[Range(0,10)]
		public int Rate { get; set; }

		[Required]
		[StringLength(10000)]
		public string Description { get; set; }

		public DateTime ReviewDate { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public virtual Film Film { get; set; }
		public long FilmId { get; set; }
	}
}
