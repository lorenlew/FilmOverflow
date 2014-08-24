using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
	public class CinemaDomainModel : EntityDomainModel
	{
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Required]
		[StringLength(60)]
		public string Address { get; set; }

		[Required]
		public string ImagePath { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		public ICollection<HallDomainModel> Halls { get; set; }

		public ICollection<SeanceDomainModel> Seances { get; set; }
	}
}
