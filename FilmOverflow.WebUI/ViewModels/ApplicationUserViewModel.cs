using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.WebUI.ViewModels
{
	public class ApplicationUserViewModel : IdentityUser
	{
		[StringLength(30)]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[StringLength(30)]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		public bool IsBanned { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }

		public ICollection<ReviewViewModel> Reviews { get; set; }

		public ICollection<ReservedSeatViewModel> ReservedSeats { get; set; }

	}
}