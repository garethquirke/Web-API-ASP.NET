using System;

namespace Weather.Models
{
    // weather info i.e. city, current temperature, current windspeed, current weather conditions, and whether a weather warning is in place
    public class WeatherInformation
    {
        // the city to which this weather information applies
        public String City
        {
            get;
            set;
        }

        // current temperature in Celsius
        public double Temperature
        {
            get;
            set;
        }

        public String Conditions
        {
            get;
            set;
        }

        // is there a weather warning in place for this city at the moment?
        public bool WeatherWarning { get; set; }
    }
}