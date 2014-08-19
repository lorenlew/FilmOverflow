﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FilmOverflow.WebUI.ViewModels
{
	public class ReviewViewModel : EntityViewModel
	{
		public int Rate { get; set; }

		[Required(ErrorMessage = "Add description")]
		[StringLength(1000)]
		public string Description { get; set; }

		public DateTime ReviewDate { get; set; }

		public string ApplicationUserId { get; set; }

		public long FilmId { get; set; }
	}
}