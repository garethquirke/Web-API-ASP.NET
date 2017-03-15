// WEbFlix public API
// GC

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebFlix.Models;

namespace WebFlix.Controllers
{
    [RoutePrefix("api/webflix")]
    public class WebFlixController : ApiController
    {
        // populate movies and and reviews..
        static private List<Movie> movies = new List<Movie>
        {
            new Movie()
            {
                ID = 1,
                Title = "Doctor Strange",
                Genres = new List<Genre> { Genre.Action, Genre.Adventure, Genre.Fantasy },
                Certificate = Certificate.Twelve,
                ReleaseDate = new DateTime(2016, 9, 1),
                Reviews = new List<Review> { new Review() { Author  = "GC", Text = "rip-roaring", Rating = 6 } }
            },
            new Movie()
            {
                ID = 2,
                Title = "Allied",
                Genres = new List<Genre> { Genre.Action, Genre.Adventure, Genre.Drama },
                Certificate = Certificate.Fifteen,
                ReleaseDate = new DateTime(2016, 11, 1),
                Reviews = new List<Review> { new Review() { Author  = "GC", Text = "a must see", Rating = 8 },
                                             new Review() { Author  = "JC", Text = "intriging", Rating = 7 }}
            },
            new Movie()
            {
                ID = 3,
                Title = "Nocturnal Animals",
                Genres = new List<Genre> { Genre.Drama, Genre.Thriller},
                Certificate = Certificate.Fifteen,
                ReleaseDate = new DateTime(2016, 10, 1),                        // no review yet
            }
        };
    

        // return all movies in reverse release date order
        [Route("movies/all")]
        public IHttpActionResult GetAllMovies()
        {
            return Ok(movies.OrderByDescending(m => m.ReleaseDate).ToList());
        }

        // return ID and title for movies matching 'keyword' as a whole word in title
        [Route("movies/title/{keyword}")]
        public IHttpActionResult GetAllMoviesByTitleKeyword(String keyword)
        {
            // return ID and title for matches whole word only
            String pattern = @"\b" + keyword.ToUpper() + @"\b";         // \bKEYWORD\b
            var results = movies.Where(m => Regex.Match(m.Title.ToUpper(), pattern).Success == true).Select(m => new { m.ID, m.Title });
            if (results.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(results);
            }
          
        }

        // get movie by ID
        [HttpGet]
        [Route("movies/id/{id:int}")]
        public IHttpActionResult GetMovieByID(int id)
        {
            try
            {
                var movie = movies.SingleOrDefault(m => m.ID == id);
                if (movie != null)
                {
                    return Ok(movie);
                }
                else
                {
                    return BadRequest("ID not found");
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // add a review for a movie
        [HttpPatch]
        [Route("movies/id/{id:int}")]
        public IHttpActionResult PatchAddReviewForMovie(int id, [FromBody] Review r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movie = movies.SingleOrDefault(m => m.ID == id);
                    if (movie != null)
                    {
                        movie.AddReview(r);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("ID not found");
                    }
                }
                catch (Exception)
                {
                    return InternalServerError();
                }
            }
            else
            {
                return BadRequest("invalid data in request");
            }
        }
    }
}
