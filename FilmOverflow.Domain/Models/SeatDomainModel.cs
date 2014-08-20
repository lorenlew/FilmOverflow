using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.Domain.Models
{
	public class SeatDomainModel : EntityDomainModel
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public int HallId { get; set; }
	}
}
