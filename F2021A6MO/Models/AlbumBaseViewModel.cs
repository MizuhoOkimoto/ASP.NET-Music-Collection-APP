using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class AlbumBaseViewModel
    {
        public AlbumBaseViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }


        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album's primary ganre")]
        public string Genre { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Coordinator who look after the album")]
        public string Coordinator { get; set; }


    }
}