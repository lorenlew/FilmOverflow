using System.Collections.Generic;

namespace FilmOverflow.WebUI.ViewModels
{
	public class CinemaRowViewModel
	{
		public CinemaViewModel Cinema { get; set; }
		public IEnumerable<FilmViewModel> Films { get; set; }		
	}
}