using GuitarAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuitarAPI.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Guitar> Guitars { get; set; }
    }
}