using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {
        public AlbumWithDetailViewModel()
        {
            Artists = new List<ArtistBaseViewModel>();
            Tracks = new List<TrackBaseViewModel>();
            ArtistName = new List<string>();
        }


        [Display(Name = "Number of Artists on this album")]
        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }

        [Display(Name = "Number of Tracks on this album")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }

        [Display(Name = "Artist name(s)")]
        public IEnumerable<string> ArtistName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album background")]
        public string Background { get; set; }

    }
}