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

        [NotMapped]        
        public string ImageEncodedBase64 { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Add duration")]
        [StringLength(60)]
        public string Duration { get; set; }

        public ICollection<ReviewDomainModel> Reviews { get; set; }

        public ICollection<SeanceDomainModel> Seances { get; set; }
    }
}