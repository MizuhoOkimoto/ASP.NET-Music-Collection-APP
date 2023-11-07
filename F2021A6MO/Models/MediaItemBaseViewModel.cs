using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class MediaItemBaseViewModel
    {
        public MediaItemBaseViewModel()
        {
            Timestamp = DateTime.Now;

            // StringId generator
            // Code is from Mads Kristensen
            // http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c
            long i = 1; //do i need it? check later!
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            StringId = string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public int MediaId { get; set; }

        [Required]
        public string Caption { get; set; }

        /*public byte[] Content { get; set; }*/

        public string ContentType { get; set; }

        // For the generated identifier
        [Required]
        public string StringId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}