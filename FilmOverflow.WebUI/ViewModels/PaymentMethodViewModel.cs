using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.WebUI.ViewModels
{
	public class PaymentMethodViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Enter name")]
		[StringLength(30)]
		public string Name { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }
	}
}