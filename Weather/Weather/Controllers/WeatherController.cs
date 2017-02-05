// a RESTFul service which reports weather information for all cities or a specified city, or reports cities for a weather warning status
// and supports weather updates for specified cities
// uses attribute based routing
// supplies Swagger metadata /swagger/docs/v1 
// and /swagger to see swagger UI test page if specified in swaggerconfig.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Weather.Models;                   // weather information model class

namespace Weather.Controllers
{
    [RoutePrefix("weather")]            // attribute based routing - override convention for all controller actions
    public class WeatherController : ApiController
    {
        /*
        * GET /weather/all                  get weather information for all cities       RetrieveAllWeatherInformation()
        * GET /weather/city/Dublin          get weather information for Dublin           GetWeatherInformationForCity(city)
        * GET /weather/cities/warning/true  get cities which have a weather warning      GetCityNameForWarningStatus(true)
         */

        private static List<WeatherInformation> weather = new List<WeatherInformation>() 
                { 
                    new WeatherInformation { City = "Dublin", Temperature = 10, Conditions = "Cloudy", WeatherWarning = false }, 
                    new WeatherInformation { City = "Cork", Temperature = 10, Conditions = "Rain", WeatherWarning = true  }, 
                    new WeatherInformation { City = "Galway", Temperature = 12, Conditions = "Sunny", WeatherWarning = false },
                    new WeatherInformation { City = "Limerick", Temperature = 8, Conditions = "Rain", WeatherWarning = true  } 
                };

        // GET /weather/all (not api/Weather)
        [Route("all")]                                                                 // on a controller action
        [HttpGet]                                                                   
        public IHttpActionResult RetrieveAllWeatherInformation()
        {
            // return all in city order, force evaluation of query using ToList()
            return Ok(weather.OrderBy(w => w.City).ToList());                                                     // 200 OK, weather serialized in response body
        }

        // GET /weather/city/Dublin
        [Route("city/{cityName:alpha}")]                                            // {parameter:constraint}
        public IHttpActionResult GetWeatherInformationForCity(String cityName)
        {
            // LINQ query, find matching city (case-insensitive) or default value (null) if none matching
            WeatherInformation cityWeather = weather.FirstOrDefault(w => w.City.ToUpper() == cityName.ToUpper());
            if (cityWeather == null)
            {
                return NotFound();                                                  // 404
            }
            return Ok(cityWeather);                                                 // 200 OK, weather info serialized in response body
        }

        [Route("cities/warning/{warning:bool}")]                                // {parameter:constraint}
        // GET api/weather/warning/true or false
        public IEnumerable<String> GetCityNameForWarningStatus(bool warning)
        {
            // LINQ query, find cities whoose weather warning status matches warning paramater
            var cities = weather.Where(w => w.WeatherWarning == warning).Select(w => w.City);
            return cities;                                                      // 200 OK, weather info serialized in response body
        }

        /* controller action return types:
            void                    - 204 (No Content)
            IHttpIHttpActionResult  - use helpers e.g Ok, NotFound etc.
            a type - 200 Ok with result serialised in response body
        */
    }
}
