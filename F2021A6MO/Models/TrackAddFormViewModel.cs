using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class TrackAddFormViewModel : TrackAddViewModel
    {
        [Required, Display(Name = "Sample clip"), DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

    }
}