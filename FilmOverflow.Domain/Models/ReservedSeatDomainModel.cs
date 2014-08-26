using System;

namespace FilmOverflow.Domain.Models
{
	public class ReservedSeatDomainModel : EntityDomainModel
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public DateTime ReservationTime { get; set; }

		public bool IsSold { get; set; }

		public SeanceDomainModel Seance { get; set; }
		public long SeanceId { get; set; }

		public string ApplicationUserId { get; set; }
	}
}
