using System;
using System.Collections.Generic;
using System.Linq;

namespace WebFlix.Models
{
    public enum Genre { Action, Adventure, Animation, Comedy, Crime, Drama, Fantasy, Family, Horror, SciFi, Thriller }
    public enum Certificate { Universal, Twelve, Fifteen, Eighteen }

   
    // a movie held by WebFlix
    public class Movie
    {
        private List<Review> reviews = new List<Review>();

        public int ID { get; set; }                         // unique
    
        public String Title { get; set; }                   // no blank
        
        public DateTime ReleaseDate { get; set; }

        public List<Genre> Genres { get; set; }             // genres

        public Certificate Certificate { get; set; }

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

        // add a review for this movie
        public void AddReview(Review review)
        {
            reviews.Add(review);
        }

        // for display to console
        public override string ToString()
        {
            String genres = "\n\tGenres: ";
            foreach (Genre g in Genres)
            {
                genres += Enum.GetName(typeof(Genre), g) + " ";
            }

            String reviews = "\n\tReviews: ";
            foreach (Review r in Reviews)
            {
                reviews += r + " ";
            }
        
            return "ID " + ID + " " + Title + " Cert: " + Certificate + " " +  " Year: " + ReleaseDate.Year + genres + reviews + "\n";
        }

    }

    // to hold anonymous type coming back from /movies/keyword/
    public class MovieWithIDandTitle
    {
        public int ID { get; set; }
        public String Title { get; set; }
    }
}