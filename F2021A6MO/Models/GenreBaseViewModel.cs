using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class GenreBaseViewModel
    {
            public int Id { get; set; }

            [Required, StringLength(100)]
            public string Name { get; set; }
    }
}