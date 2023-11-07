using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class MediaItemContentViewModel
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}