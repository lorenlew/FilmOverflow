using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmOverflow.WebUI.ViewModels.Seance
{
	public class CreateSeanceViewModel : SeanceViewModel
	{
		[Required(ErrorMessage = "Add date")]
		[Display(Name = "End Date")]
		[RegularExpression(@"(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])([1-2][0,9][0-9][0-9]))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])([1-2][0,9][0-9][0-9]))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])([1-2][0,9][0-9][0-9]))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))", ErrorMessage = "Date is not valid")]
		public string EndDate { get; set; }

		[Display(Name = "Multiple date select")]
		public bool IsMultipleDateSelect { get; set; }
	}
}