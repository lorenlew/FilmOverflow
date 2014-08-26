using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.WebUI.ViewModels
{
	public class SeanceViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Add date")]
		//[RegularExpression(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$",ErrorMessage = "Date is not valid")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required(ErrorMessage = "Add time")]
		[DataType(DataType.Time)]
		public DateTime Time { get; set; }

		[Required(ErrorMessage = "Add price")]
		[Range(0, 256)]
		public decimal Price { get; set; }

		public long FilmId { get; set; }

		public HallViewModel Hall { get; set; }
		[Required(ErrorMessage = "Select hall")]
		public long HallId { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }

		public ICollection<ReservedSeatViewModel> ReservedSeats { get; set; }
	}
}