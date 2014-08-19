using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.Domain.Models
{
	public class PaymentMethodDomainModel : EntityDomainModel
	{
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		public ICollection<TicketDomainModel> Tickets { get; set; }
	}
}
