using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarAPI.Models
{
    public class Guitar
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public bool IsNew { get; set; }
        public int Stock { get; set; }
    }
}