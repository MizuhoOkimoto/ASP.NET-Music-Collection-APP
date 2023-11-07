using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {
            public ArtistWithDetailViewModel()
            {
                Albums = new List<AlbumBaseViewModel>();
            }

            [Display(Name = "Number of albums")]
            public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Artist career")]
            public string Career { get; set; }
    }
}