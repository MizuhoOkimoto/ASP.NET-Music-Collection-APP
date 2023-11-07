using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class MediaItemAddViewModel
    {
        //It MUST have the artist identifier,
        //and the properties that capture information and data for the media item.
        public int ArtistId { get; set; }

        [Required]
        public string ArtistName { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }
    }
}