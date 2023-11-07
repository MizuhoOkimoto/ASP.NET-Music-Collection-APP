using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    //View model class for an object with a media info collection
    public class ArtistWithMediaInfoViewModel : ArtistWithDetailViewModel
    {
        public ArtistWithMediaInfoViewModel()
        {
            MediaItems = new List<MediaItemBaseViewModel>();
        }


        public IEnumerable<MediaItemBaseViewModel> MediaItems { get; set; }
    
    }
}