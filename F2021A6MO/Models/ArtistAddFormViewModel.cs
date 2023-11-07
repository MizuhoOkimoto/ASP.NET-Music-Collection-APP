using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Models
{
    public class ArtistAddFormViewModel : ArtistAddViewModel
    {
        public string ArtistName { get; set; }

        //Lecture5 ppt page12
        [Display(Name = "Artist's primary genre")]
        public SelectList ArtistGenreList { get; set; }

}
}