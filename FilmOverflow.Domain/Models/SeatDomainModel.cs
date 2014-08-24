namespace FilmOverflow.Domain.Models
{
	public class SeatDomainModel : EntityDomainModel
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public int HallId { get; set; }
	}
}
