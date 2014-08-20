using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.Domain.Models
{
	public class HallDomainModel : EntityDomainModel
	{
		public int HallNumber { get; set; }

		public int RowAmount { get; set; }

		public int ColumnAmount { get; set; }

		public ICollection<SeatDomainModel> Seats { get; set; }

		public ICollection<CinemaDomainModel> Cinemas { get; set; }
	}
}
