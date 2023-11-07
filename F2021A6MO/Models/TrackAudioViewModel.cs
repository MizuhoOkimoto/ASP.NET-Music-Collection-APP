using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A6MO.Models
{
    public class TrackAudioViewModel
    {
        public int Id { get; set; }
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }
    }
}