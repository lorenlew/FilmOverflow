using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.WebUI.ViewModels
{
	public class ReviewViewModel : EntityViewModel
	{
		[Range(0, 10)]
		public int Rate { get; set; }

		[Required(ErrorMessage = "Add description")]
		[StringLength(1000)]
		public string Description { get; set; }

		public DateTime ReviewDate { get; set; }

		public ApplicationUserViewModel ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public FilmViewModel Film { get; set; }
		public long FilmId { get; set; }
	}
}