using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmOverflow.WebUI.ViewModels.Seance;

namespace FilmOverflow.WebUI.ViewModels
{
	public class HallViewModel : EntityViewModel
	{
		[Required]
		[StringLength(60)]
		public string Name { get; set; }

		[Display(Name = "Row amount")]
		[Range(1, 50)]
		public int RowAmount { get; set; }

		[Display(Name = "Column amount")]
		[Range(1, 50)]
		public int ColumnAmount { get; set; }

		public CinemaViewModel Cinema { get; set; }
		public long CinemaId { get; set; }

		public ICollection<SeatViewModel> Seats { get; set; }

		public ICollection<SeanceViewModel> Seances { get; set; }
	}
}