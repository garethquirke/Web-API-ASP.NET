using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CAPractice.Models;


namespace CAPractice.Controllers
{
    [RoutePrefix("phonebook")]
    public class PhoneBookController : ApiController
    {
        List<PhoneBook> contacts;

        // GET /api/phonebook/number
        // GET /api/phonebook?name=
        public PhoneBookController()
        {
            contacts = new List<PhoneBook>();
            contacts.Add(new PhoneBook() { Number = "12509", Name = "Gareth Quirke", Address = "101 drive road" });
            contacts.Add(new PhoneBook() { Number = "12234", Name = "Tom Jones", Address = "209 stockton" });
            contacts.Add(new PhoneBook() { Number = "08776", Name = "Joe Rogan", Address = "crumlin 4 road" });
            contacts.Add(new PhoneBook() { Number = "45782", Name = "Tim Collins", Address = "dublin 12" });
            contacts.Add(new PhoneBook() { Number = "34567", Name = "Bob Marley", Address = "tallaght village" });
        }


        [Route("number/{number}")]
        // GET phonebook/number/01 1111111
        public IHttpActionResult GetEntry(String number)
        {
            // LINQ query, find matching entry for number
            var entry = contacts.FirstOrDefault(e => e.Number.ToUpper() == number.ToUpper());
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [Route("name/{name}")]
        // GET phonebook/name/Jane Doe
        public IHttpActionResult GetEntriesForName(String name)
        {
            // LINQ query, find matching entries for name
            var entries = contacts.Where(r => r.Name.ToUpper() == name.ToUpper());
            if (entries == null)
            {
                return NotFound();
            }
            return Ok(entries);
        }
    }
}