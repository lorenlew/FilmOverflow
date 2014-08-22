using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.WebUI.ViewModels
{
	public class SeanceViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Add date")]
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		[Required(ErrorMessage = "Add time")]
		[DataType(DataType.DateTime)]
		public DateTime Time { get; set; }

		[Required(ErrorMessage = "Add price")]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public long FilmId { get; set; }

		public HallViewModel Hall { get; set; }
		[Required(ErrorMessage = "Select hall")]
		public long HallId { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }

		public ICollection<ReservedSeatViewModel> ReservedSeats { get; set; }
	}
}