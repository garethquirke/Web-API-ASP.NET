using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EAD2CA1Lab2.Models
{
    public class Weather
    {
        [Required(ErrorMessage ="Invalid City")]
        public string City { get; set; }


        [Range(-50, 50, ErrorMessage ="Invalid Temperature")]
        public double Temperature { get; set; }


        [Range(0, 200, ErrorMessage = "Invalid Wind Speed")]
        public int WindSpeed { get; set; }


        [Required(ErrorMessage ="Invaid Conditions")]
        public string Conditions { get; set; }


        public bool Warning { get; set; }
    }
}