using F2021A6MO.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Models
{
    public class TrackAddViewModel
    {
        public TrackAddViewModel()
        {
            Albums = new List<Album>();
        }

        [Required, StringLength(100)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [StringLength(100)]
        public string Clerk { get; set; }

        [Required, StringLength(500)]
        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        //************************************************
        [Display(Name = "Album")]
        [Required(ErrorMessage = "Album must be checked")]
        public int AlbumId { get; set; } //add
        public string AlbumName { get; set; } //add

        [Display(Name = "Album list")]
        public MultiSelectList AlbumList { get; set; }

        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }

        //************************************************
        public IEnumerable<Album> Albums { get; set; }

        //************************************************
        [Required]
        [Display(Name = "Sample clip"), DataType(DataType.Upload)]
        public HttpPostedFileBase AudioUpload { get; set; }
    }
}