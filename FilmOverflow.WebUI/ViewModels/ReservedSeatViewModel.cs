using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmOverflow.WebUI.ViewModels
{
	public class ReservedSeatViewModel : EntityViewModel
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public bool IsSold { get; set; }

		public DateTime ReservationTime { get; set; }

		public long SeanceId { get; set; }
	}
}