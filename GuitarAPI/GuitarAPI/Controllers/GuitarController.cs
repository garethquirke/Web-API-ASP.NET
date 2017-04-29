using GuitarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using GuitarAPI.DAL;


namespace GuitarAPI.Controllers
{
    [RoutePrefix("guitar")]
    public class GuitarController : ApiController
    {

        private GuitarRepository repo = new GuitarRepository();
        /* ID, Name, Make, isNew
         * 
         * 
         * GET /guitar/all
         * GET /guitar/name
         */

        // GET: /guitar/all
        [Route("all")]
        public IEnumerable<Guitar> GetAllGuitars()
        {
            return repo.GetAllGuitars();
        }

        // GET: /guitar/name/Mustang
        [Route("name/{name:alpha}")]
        public Guitar GetGuitarByName(string name)
        {
            Guitar guitar = repo.GetGuitarByName(name);
            if (guitar == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return guitar;
        }

        // GET: /guitar/new
        // get guitars that are new
        [Route("new")]
        public IEnumerable<Guitar> GetNewGuitars()
        {
            var guitars = repo.GetNewGuitars();
            return guitars;
        }

        // POST a new guitar
        [Route("")]
        public IHttpActionResult PostGuitar(Guitar guitar)
        {
            if (ModelState.IsValid)
            {

                int i = repo.AddGuitar(guitar);
                if(i == 1)
                {
                    string uri = Request.RequestUri.ToString() + "id/" + guitar.Name;
                    return Created(uri, guitar);
                }
                else
                {
                    // the stock has been updated
                    string uri = Request.RequestUri.ToString() + "id/" + guitar.Name + " has had  it's stock updated";
                    return Created(uri, guitar);
                }

            }
            else
            {
                return BadRequest();
            }
        }


        public IHttpActionResult DeleteGuitar(string name)
        {
            int t = repo.DeleteGuitar(name);
            if(t == 1)
            {
                return Ok(repo.GetAllGuitars());
            }
            return NotFound();

        }







    }
}