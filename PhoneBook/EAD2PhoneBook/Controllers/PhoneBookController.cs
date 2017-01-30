using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EAD2PhoneBook.Models;

namespace EAD2PhoneBook.Controllers
{
    public class PhoneBookController : ApiController
    {
        private List<PhoneBook> contacts;

        public PhoneBookController()
        {
            contacts = new List<PhoneBook>()
            {
                new PhoneBook { Name = "Gareth Quirke", Address = "22 wilton drive", Number = 1200090 },
                new PhoneBook { Name = "Bob Quirke", Address = "22 dumbo drive", Number = 1200870 },
                new PhoneBook { Name = "Jimbo Quirke", Address = "22 timmo drive", Number = 1204090 },
                new PhoneBook { Name = "Timbo Quirke", Address = "22 zimmos drive", Number = 1350090 },
                new PhoneBook { Name = "Jake Quirke", Address = "22 rambo drive", Number = 1209090 },
                new PhoneBook { Name = "Shane Quirke", Address = "22 fikkio drive", Number = 1265090 }
            };
        }


        public IHttpActionResult GetAllContacts()
        {
            return Ok(contacts.OrderBy(c => c.Name).ToList());
        }
    }
}
