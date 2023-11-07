using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class TrackEditFormViewModel
    {
        public string Name { get; set; }

        [Required, Display(Name = "Clip"), DataType(DataType.Upload)]
        public string AudioUpload { get; set; }
    }
}