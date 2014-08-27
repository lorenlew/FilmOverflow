using System;
using FilmOverflow.WebUI.ViewModels.Seance;

namespace FilmOverflow.WebUI.ViewModels
{
	public class ReservedSeatViewModel : EntityViewModel
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public DateTime ReservationTime { get; set; }

		public bool IsSold { get; set; }

		public SeanceViewModel Seance { get; set; }
		public long SeanceId { get; set; }

		public string ApplicationUserId { get; set; }
	}
}