using System;

namespace FilmOverflow.DAL.Models
{
	public class ReservedSeat : Entity
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public DateTime ReservationTime { get; set; }

		public virtual Seance Seance { get; set; }
		public long SeanceId { get; set; }

	}
}
