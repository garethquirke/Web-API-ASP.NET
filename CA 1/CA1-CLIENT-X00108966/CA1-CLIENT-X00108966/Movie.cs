using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1_CLIENT_X00108966
{
    public class Movie
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public List<Genres> Genre { get; set; }
        public string Certification { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Review Review { get; set; }
        public double AverageRating { get; set; }

        public string GenreToString()
        {
            string genres = string.Join(",", Genre.ToArray());
            return genres;
        }

        public override string ToString()
        {
            return "ID: " + ID + " Title: " + Title + " Genre: " + GenreToString() + " Certification: " + Certification + " Release Date: " + ReleaseDate.ToString("dd/MM/yy H:mm:ss zzz") 
                +" Author: " + Review.Author + " Opinion: " + Review.Text + " Rating: "+ Review.Rating  + " Avg Rating: " + AverageRating;
        }
    }

    public class Review
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}

public enum Genres { action, adventure, animation, comedy, crime, drama, fantasy, family, scifi, thriller }

