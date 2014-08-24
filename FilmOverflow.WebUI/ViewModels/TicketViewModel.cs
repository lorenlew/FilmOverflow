using System;

namespace FilmOverflow.WebUI.ViewModels
{
	public class TicketViewModel : EntityViewModel
	{
		public DateTime PaymentDate { get; set; }

		public string ApplicationUserId { get; set; }

		public long SeanceId { get; set; }

		public long SeatId { get; set; }

		public long PaymentMethodId { get; set; }
	}
}