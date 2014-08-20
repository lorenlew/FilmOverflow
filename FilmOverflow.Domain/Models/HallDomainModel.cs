using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.Domain.Models
{
	public class HallDomainModel : EntityDomainModel
	{
		[Required]
		[StringLength(60)]
		public string Name { get; set; }

		public int RowAmount { get; set; }

		public int ColumnAmount { get; set; }

		public long CinemaId { get; set; }

		public ICollection<SeatDomainModel> Seats { get; set; }

		public ICollection<SeanceDomainModel> Seances { get; set; }
	}
}
