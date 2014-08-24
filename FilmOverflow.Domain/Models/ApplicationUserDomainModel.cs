using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.Domain.Models
{
	public class ApplicationUserDomainModel : IdentityUser
	{
		[StringLength(30)]
		[Required]
		public string FirstName { get; set; }

		[StringLength(30)]
		[Required]
		public string LastName { get; set; }

		public bool IsBanned { get; set; }

		public ICollection<TicketDomainModel> Tickets { get; set; }

		public ICollection<ReviewDomainModel> Reviews { get; set; }

	}
}