using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.Domain.Models
{
	public class HallDomainModel : EntityDomainModel
	{
		[Required]
		[StringLength(60)]
		public string Name { get; set; }

		[Range(1, 50)]
		public int RowAmount { get; set; }

		[Range(1, 50)]
		public int ColumnAmount { get; set; }

		public CinemaDomainModel Cinema { get; set; }
		public long CinemaId { get; set; }

		public ICollection<SeatDomainModel> Seats { get; set; }

		public ICollection<SeanceDomainModel> Seances { get; set; }
	}
}
