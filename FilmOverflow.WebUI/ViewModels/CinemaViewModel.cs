using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using FilmOverflow.Domain.Models;
using FilmOverflow.WebUI.Attributes;

namespace FilmOverflow.WebUI.ViewModels
{
	public class CinemaViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Enter name")]
		[StringLength(30)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Enter address")]
		[StringLength(60)]
		public string Address { get; set; }

		[NotMapped]
		[FileSize(15360000)]
		[FileTypes("jpg,jpeg,png")]
		[Required(ErrorMessage = "Upload image")]
		public HttpPostedFileBase Image { get; set; }

		[Display(Name = "Image")]
		public string ImagePath { get; set; }

		[Required]
		[Display(Name = "Enter phone number")]
		public string PhoneNumber { get; set; }

		public ICollection<SeanceViewModel> Seances { get; set; }

		public ICollection<HallViewModel> Halls { get; set; }
	}
}