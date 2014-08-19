using System;

namespace FilmOverflow.WebUI.ViewModels
{
	public class TicketViewModel : EntityViewModel
	{
		public string ApplicationUserId { get; set; }

		public long SeanceId { get; set; }

		public ReservedSeatViewModel ReservedSeat { get; set; }

		public long PaymentMethodId { get; set; }

		public DateTime PaymentDate { get; set; }
	}
}