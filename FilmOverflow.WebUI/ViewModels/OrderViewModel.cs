using System;
using System.Collections.Generic;

namespace FilmOverflow.WebUI.ViewModels
{
	public class OrderViewModel 
	{
		public long SeanceId { get; set; }

		public long PaymentMethodId { get; set; }

		public ICollection<SeatViewModel> Seats { get; set; }
	}
}