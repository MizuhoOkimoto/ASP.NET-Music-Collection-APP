using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Models
{
    public class AlbumAddFormViewModel : AlbumAddViewModel
    {
        /*public MultiSelectList ArtistNameList { get; set; }


        public MultiSelectList TrackList { get; set; }*/

        [Display(Name = "Album's Primary genre")]
        public SelectList AlbumGenreList { get; set; } //add

        public string ArtistName { get; set; }
    }
}