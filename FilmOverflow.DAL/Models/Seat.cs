using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmOverflow.DAL.Models
{
	public class Seat : Entity
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public bool IsProcessing { get; set; }

		public virtual Hall Hall { get; set; }
		public int HallId { get; set; }
	}
}
