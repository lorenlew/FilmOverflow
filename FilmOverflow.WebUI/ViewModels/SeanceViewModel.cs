using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.WebUI.ViewModels
{
	public class SeanceViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Add date")]
		//[RegularExpression(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$",ErrorMessage = "Date is not valid")]
		public string Date { get; set; }

		[Required(ErrorMessage = "Add time")]
		public string Time { get; set; }

		[Required(ErrorMessage = "Add price")]
		public decimal Price { get; set; }

		public long FilmId { get; set; }

		public HallViewModel Hall { get; set; }
		[Required(ErrorMessage = "Select hall")]
		public long HallId { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }

		public ICollection<ReservedSeatViewModel> ReservedSeats { get; set; }
	}
}