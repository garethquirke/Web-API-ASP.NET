using GuitarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace GuitarAPI.Controllers
{
    [RoutePrefix("guitar")]
    public class GuitarController : ApiController
    {
        /* ID, Name, Make, isNew
         * 
         * 
         * GET /api/guitar/
         * GET /api/guitar/name
         */

        private static List<Guitar> inventory = new List<Guitar>()
        {
            new Guitar { ID = 1, Name = "Strat", Make = "Fender", IsNew = true, Stock = 3 },
            new Guitar { ID = 2, Name = "Tele", Make = "Fender", IsNew = true, Stock = 2 },
            new Guitar { ID = 3, Name = "SG", Make = "Gibson", IsNew = false, Stock = 1 },
            new Guitar { ID = 4, Name = "Mustang", Make = "Fender", IsNew = true, Stock = 5 },
            new Guitar { ID = 5, Name = "HummingBird", Make = "Gibson", IsNew = false, Stock = 3 }
        };
      
        // GET: /guitar/all
        [Route("all")]
        public IEnumerable<Guitar> GetAllGuitars()
        {
            return inventory;
        }

        // GET: /guitar/name/Mustang
        [Route("name/{name:alpha}")]
        public Guitar GetGuitarByName(string name)
        {
            Guitar guitar = inventory.FirstOrDefault(g => g.Name.ToUpper() == name.ToUpper());
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
            var guitars = inventory.Where(g => g.IsNew == true);
            return guitars;
        }

        // POST a new guitar
        [Route("")]
        public IHttpActionResult PostGuitar(Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                lock (inventory)
                {
                    // check the stock see if its already there
                    var stock = inventory.SingleOrDefault(g => g.Name.ToUpper() == guitar.Name.ToUpper());
                    if (stock == null)
                    {
                        inventory.Add(guitar);

                        string uri = Request.RequestUri.ToString() + "id/" + guitar.Name;
                        // string uri = HttpReque

                        return Created(uri, guitar);
                    }
                    else
                    {
                        stock.Stock++;
                        string uri = Request.RequestUri.ToString() + "id/" + guitar.Name + " has been stock updated";
                        return Created(uri, guitar);
                    }
                }
            }
            else
            {
                return BadRequest();
            }
        }



        public IHttpActionResult DeleteGuitar(string name)
        {
            lock (inventory)
            {
                var record = inventory.SingleOrDefault(g => g.Name.ToUpper() == name.ToUpper());
                if(record != null)
                {
                    inventory.Remove(record);
                    return Ok(inventory.OrderBy(g => g.Name).ToList());
                }
                else
                {
                    return NotFound();
                }
            }
        }



    }
}