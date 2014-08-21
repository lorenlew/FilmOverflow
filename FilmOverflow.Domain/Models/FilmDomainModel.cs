using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace FilmOverflow.Domain.Models
{
	public class FilmDomainModel : EntityDomainModel
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

		public ICollection<ReviewDomainModel> Reviews { get; set; }

		public ICollection<SeanceDomainModel> Seances { get; set; }
	}
}
