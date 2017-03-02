using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CA1_API_X00108966.Models
{
    public class Movie
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Genres> Genre { get; set; }

        public string Certification { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Review Review { get; set; }
        // Read only property
        public double AverageRating
        {
            get
            {
                return Review.Rating / Review.Rating.ToString().Count();
            }
            
        }
    }

    public class Review
    {
        public string Author { get; set; }
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Must be between 5 - 100")]
        public string Text { get; set; }
        [Range(1, 10, ErrorMessage = "Values between 1 to 10 only")]
        public int Rating { get; set; }
    }
}

public enum Genres { action, adventure, animation, comedy, crime, drama, fantasy, family, scifi, thriller }