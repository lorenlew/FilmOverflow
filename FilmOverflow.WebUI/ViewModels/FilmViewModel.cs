using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using FilmOverflow.Domain.Models;
using FilmOverflow.WebUI.Attributes;

namespace FilmOverflow.WebUI.ViewModels
{
	public class FilmViewModel : EntityViewModel
	{
		[Required(ErrorMessage = "Add title")]
		[StringLength(60)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Add description")]
		[StringLength(1000)]
		public string Description { get; set; }

		[FileSize(15360000)]
		[FileTypes("jpg,jpeg,png")]
		public HttpPostedFileBase Image { get; set; }

		[Display(Name = "Image")]
		public string ImagePath { get; set; }

		[Required(ErrorMessage = "Add duration")]
		[Range(0, 300, ErrorMessage = "Out of range")]
		public int Duration { get; set; }

		public ICollection<ReviewViewModel> Reviews { get; set; }

		public ICollection<SeanceViewModel> Seances { get; set; }
	}
}