using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.DAL.Models
{
	public class Hall : Entity
	{
		public int HallNumber { get; set; }

		public int RowAmount { get; set; }

		public int ColumnAmount { get; set; }

		public ICollection<Seat> Seats { get; set; }

		public ICollection<Cinema> Cinemas { get; set; }
	}
}
