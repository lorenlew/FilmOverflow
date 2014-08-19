using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmOverflow.WebUI.ViewModels
{
	public class HallViewModel : EntityViewModel
	{
		public int HallNumber { get; set; }

		public int RowAmount { get; set; }

		public int ColumnAmount { get; set; }

		public ICollection<SeatViewModel> Seats { get; set; }

		public ICollection<CinemaViewModel> Cinemas { get; set; }
	}
}