using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.DAL.Models
{
	public class ReservedSeat : Entity
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public bool IsSold { get; set; }

		public DateTime ReservationTime { get; set; }

		[Key, ForeignKey("Ticket")]
		public new long Id { get; set; }
		public virtual Ticket Ticket { get; set; }

		public virtual Seance Seance { get; set; }
		public long SeanceId { get; set; }

	}
}
