using System;
using System.ComponentModel.DataAnnotations;
using FilmOverflow.WebUI.ViewModels.Seance;

namespace FilmOverflow.WebUI.ViewModels
{
	public class TicketViewModel : EntityViewModel
	{
		[Display(Name = "Date")]
		public DateTime PaymentDate { get; set; }

		public ApplicationUserViewModel ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public SeanceViewModel Seance{ get; set; }
		public long SeanceId { get; set; }

		[Display(Name = "Payment Method")]
		public PaymentMethodViewModel PaymentMethod { get; set; }
		public long PaymentMethodId { get; set; }
		
		public long SeatId { get; set; }
	}
}