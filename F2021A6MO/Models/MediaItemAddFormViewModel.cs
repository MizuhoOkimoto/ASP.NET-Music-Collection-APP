using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class MediaItemAddFormViewModel : MediaItemAddViewModel
    {
        //As you have learned it MUST have an artist identifier,
        //and it SHOULD have some descriptive information about the artist, to display on the HTML Form.
        [Required, Display(Name = "Media upload"), DataType(DataType.Upload)]
        public string MediaUpload { get; set; }

    }
}