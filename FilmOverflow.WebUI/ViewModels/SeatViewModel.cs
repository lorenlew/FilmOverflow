using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmOverflow.WebUI.ViewModels
{
	public class SeatViewModel : EntityViewModel
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public int HallId { get; set; }
	}
}