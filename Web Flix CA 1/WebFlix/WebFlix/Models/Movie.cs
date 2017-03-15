// a movie

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebFlix.Models
{
    // various genres
    public enum Genre { Action, Adventure, Animation, Comedy, Crime, Drama, Fantasy, Family, Horror, SciFi, Thriller }

    // various certs
    public enum Certificate { Universal, Twelve, Fifteen, Eighteen }


    public class Movie
    {
        public int ID { get; set; }                         // unique

        [Required]
        public String Title { get; set; }         
      
        public DateTime ReleaseDate { get; set; }           // year and month
                
        public int Duration { get; set; }                   // minutes

        public List<Genre> Genres { get; set; }             // genres

        public Certificate Certificate { get; set; }

        private List<Review> reviews = new List<Review>();
        public List<Review> Reviews
        {
            get
            {
                return reviews;
            }
            set
            {
                reviews = value;
            }
        }
        public double? AverageRating                        // 1 .. 10, or null if no review
        {
            get
            {
                if (reviews.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return reviews.Average(r => r.Rating);
                }
              
            }
        }     
                            
        public void AddReview(Review review)
        {
            reviews.Add(review);
        }

    }
}