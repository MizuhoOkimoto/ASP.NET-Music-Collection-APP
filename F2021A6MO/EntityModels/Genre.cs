﻿using System.ComponentModel.DataAnnotations;

namespace F2021A6MO.EntityModels
{
    public class Genre
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}