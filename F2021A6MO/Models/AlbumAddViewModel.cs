using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class AlbumAddViewModel
    {
        public AlbumAddViewModel()
        {
            ReleaseDate = DateTime.Now;
            ArtistIds = new List<int>();
        }


        [StringLength(100)]
        public string Coordinator { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Url to album image (cover art)")]
        [Required, StringLength(500)]
        public string UrlAlbum { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album background")]
        public string Background { get; set; }


        //*************************************************

        [Display(Name = "Artists")]
        public IEnumerable<int> ArtistIds { get; set; }

        //*************************************************
    }
}