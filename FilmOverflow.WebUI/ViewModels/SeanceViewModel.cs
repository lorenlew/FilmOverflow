using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.WebUI.ViewModels
{
	public class SeanceViewModel : EntityViewModel
	{
		public DateTime Date { get; set; }

		public DateTime Time { get; set; }

		public decimal Price { get; set; }

		public string FilmId { get; set; }

		public long CinemaId { get; set; }

		public ICollection<TicketViewModel> Tickets { get; set; }		
	}
}