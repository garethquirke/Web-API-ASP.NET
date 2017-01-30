using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAD2PhoneBook.Models
{
    public class PhoneBook
    {
        public string Name { get; set; }

        public string Address { get; set; }

        // unique number
        [Key]
        public int Number { get; set; }
    }
}