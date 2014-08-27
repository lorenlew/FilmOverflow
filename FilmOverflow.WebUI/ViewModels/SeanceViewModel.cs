using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.WebUI.ViewModels
{
	public class SeanceViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Add date")]
		[RegularExpression(@"(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])([1-2][0,9][0-9][0-9]))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])([1-2][0,9][0-9][0-9]))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])([1-2][0,9][0-9][0-9]))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))", ErrorMessage = "Date is not valid")]
		public string Date { get; set; }

		[Required(ErrorMessage = "Add time")]
		[DataType(DataType.Time)]
		public string Time { get; set; }

		[Required(ErrorMessage = "Add price")]
		[Range(0, 256)]
		public decimal Price { get; set; }

		public FilmViewModel Film { get; set; }
		public long FilmId { get; set; }

		public HallViewModel Hall { get; set; }
		[Required(ErrorMessage = "Select hall")]
		public long HallId { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }

		public ICollection<ReservedSeatViewModel> ReservedSeats { get; set; }
	}
}