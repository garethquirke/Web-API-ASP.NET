using System;

namespace WebFlix.Models
{
    public class Review
    {
        public String Author { get; set; }
        public String Text { get; set; }
        public int Rating { get; set; }

        public override string ToString()
        {
            return Author + " " + Text + " " + " Rating: " + Rating;
        }
    }
}