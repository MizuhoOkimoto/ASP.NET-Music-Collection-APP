using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class ArtistBaseViewModel
    {
        public ArtistBaseViewModel()
        {
            BirthOrStartDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "if applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }


        [Required, StringLength(100)]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Executives who looks after this artist")]
        public string Executive { get; set; }

    }
}