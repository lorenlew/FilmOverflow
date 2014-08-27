using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using FilmOverflow.WebUI.Attributes;
using FilmOverflow.WebUI.ViewModels.Seance;

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

		[FileSize(15360000)]
		[FileTypes("jpg,jpeg,png")]
		[Required(ErrorMessage = "Upload image")]
		public HttpPostedFileBase Image { get; set; }

		[Display(Name = "Image")]
		public string ImagePath { get; set; }

		[Required(ErrorMessage = "Enter phone number")]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; }

		public ICollection<SeanceViewModel> Seances { get; set; }

		public ICollection<HallViewModel> Halls { get; set; }
	}
}