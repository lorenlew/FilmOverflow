using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.DAL.Models
{
	public class Hall : Entity
	{
		[Required]
		[StringLength(60)]
		public string Name { get; set; }

		public int RowAmount { get; set; }

		public int ColumnAmount { get; set; }

		public virtual Cinema Cinema { get; set; }
		public long CinemaId { get; set; }

		public virtual ICollection<Seat> Seats { get; set; }

		public virtual ICollection<Seance> Seances { get; set; }
	}
}
