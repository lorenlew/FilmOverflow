using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
	public class ReviewDomainModel : EntityDomainModel
	{
		[Range(0, 10)]
		public int Rate { get; set; }

		[Required]
		[StringLength(1000)]
		public string Description { get; set; }

		public DateTime ReviewDate { get; set; }

		public ApplicationUserDomainModel ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public FilmDomainModel Film { get; set; }
		public long FilmId { get; set; }
	}
}
