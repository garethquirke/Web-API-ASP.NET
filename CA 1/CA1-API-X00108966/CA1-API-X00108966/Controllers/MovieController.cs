using CA1_API_X00108966.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CA1_API_X00108966.Controllers
{
    [RoutePrefix("movie")]
    public class MovieController : ApiController
    {


        static private List<Movie> catalog = new List<Movie>()
        {
            new Movie { ID = 1, Title = "Scarface", Genre = new List<Genres> { Genres.crime, Genres.fantasy }, Certification = "Universal", ReleaseDate = DateTime.Now, Review = new Review { Author = "Gareth", Text = "great", Rating = 8} },
            new Movie { ID = 2, Title = "Toy Story", Genre = new List<Genres> { Genres.comedy, Genres.animation }, Certification = "Universal", ReleaseDate = DateTime.Now, Review = new Review { Author = "Gareth", Text = "Unreal", Rating = 9}   },
            new Movie { ID = 3, Title = "Money Ball", Genre = new List<Genres> { Genres.crime, Genres.fantasy }, Certification = "Universal", ReleaseDate = DateTime.Now, Review = new Review { Author = "Gareth", Text = "it was okay", Rating = 3}  },

        };

        //public MovieController()
        //{
        //    SetAverage(catalog);
        //}
        //public void SetAverage(List<Movie> catalog)
        //{
        //    catalog.Select(r => r.AverageRating = GetAverage());
        //}

        //public double GetAverage()
        //{

        //    var avg = catalog.Select(r => r.Review.Rating).Average();

        //    return avg;
        //}

        [Route("all")]
        public IHttpActionResult GetAllMovies()
        {
            lock (catalog)
            {
                var list = catalog.OrderByDescending(m => m.ReleaseDate);
                return Ok(list);
            }
        }


        [Route("id/{id:int}")]
        public IHttpActionResult GetMovieByID(int id)
        {
            lock (catalog)
            {
                var movie = catalog.FirstOrDefault(m => m.ID == id);
                if(movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
        }



        [Route("search/{title:alpha}")]
        public IHttpActionResult GetMovieByKeyword(string keyword)
        {
            lock (catalog)
            {
                var movie = catalog.FindAll(m => m.Title.ToUpper().Contains(keyword));
                if(movie == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return Ok(movie);
            }
        }



        [Route("")]
        public IHttpActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                Movie movie = new Movie { ID = 4, Title = "Friday", Genre = new List<Genres> { Genres.adventure }, Certification = "UNIVERSAL", ReleaseDate = DateTime.Now, Review = review};
                lock (catalog)
                {
                    catalog.Add(movie);
                    string uri = Request.RequestUri.ToString() + "id/" + movie.ID;
                    return Created(uri, movie);
                }
            }
            else
            {
                return BadRequest();
            }
            
        }

    }
}