using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.DAL.Models
{
	public class PaymentMethod : Entity
	{
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		public virtual ICollection<Ticket> Tickets { get; set; }
	}
}
