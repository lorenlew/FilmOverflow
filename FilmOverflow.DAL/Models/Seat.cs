namespace FilmOverflow.DAL.Models
{
	public class Seat : Entity
	{
		public int RowNumber { get; set; }

		public int ColumnNumber { get; set; }

		public virtual Hall Hall { get; set; }
		public long HallId { get; set; }

	}
}
