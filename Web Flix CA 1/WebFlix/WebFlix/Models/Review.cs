// a review of a movie

using System;
using System.ComponentModel.DataAnnotations;

namespace WebFlix.Models
{
    public class Review
    {
        [Required]
        public String Author { get; set; }

        [StringLength(100, MinimumLength = 5)]
        public String Text { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }
    }
}