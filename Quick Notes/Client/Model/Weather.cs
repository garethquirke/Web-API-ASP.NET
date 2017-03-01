using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClient
{
    class Weather
    {
        public string City { get; set; }

        public double Temperature { get; set; }
        public int WindSpeed { get; set; }

        public string Conditions { get; set; }

        public bool WeatherWarning { get; set; }
    }
}
