using F2021A6MO.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class TrackBaseViewModel
    {
        public TrackBaseViewModel()
        {
            Albums = new List<Album>();
            AlbumNames = new List<string>();
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Required, StringLength(500)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track genre")]
        public string Genre { get; set; }



        /*[Display(Name = "Album name")]
        public AlbumBaseViewModel AlbumName { get; set; }*/ //???

        //---------------------------------------------------
        [Display(Name = "Album name")]
        public IEnumerable<string> AlbumNames { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public IEnumerable<Album> Albums { get; set; }
    }
}