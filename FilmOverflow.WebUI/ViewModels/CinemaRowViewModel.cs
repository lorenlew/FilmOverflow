using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmOverflow.WebUI.ViewModels
{
	public class CinemaRowViewModel
	{
		public CinemaViewModel Cinema { get; set; }
		public IEnumerable<FilmViewModel> Films { get; set; }		
	}
}