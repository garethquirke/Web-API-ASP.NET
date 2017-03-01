using EAD2CA1Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAD2CA1Lab2.Controllers
{
    public class WeatherController : ApiController
    {
        /*
        * GET /api/weather                  get weather information for all cities       GetAllWeatherInformation()
        * GET /api/weather/city             get weather information for city             GetWeatherInformationForCity(city)
        * GET /api/weather?warning=true     get cities which have a weather warning      GetCitiesForWarningStatus(true)
          PUT /api/weather                  update weather information for city          PutWeatherInformationForCity(city)
         */

        private static List<Weather> forecasts = new List<Weather>()
                {
                    new Weather { City = "Dublin", Temperature = 10, WindSpeed = 30, Conditions = "Cloudy", Warning = false },
                    new Weather { City = "Cork", Temperature = 10, WindSpeed = 50, Conditions = "Rain", Warning = true  },
                    new Weather { City = "Galway", Temperature = 12, WindSpeed = 10, Conditions = "Sunny", Warning = false },
                    new Weather { City = "Limerick", Temperature = 8, WindSpeed = 50, Conditions = "Rain", Warning = true  }
                };

        public IEnumerable<Weather> GetAllWeatherForecasts()
        {
            return forecasts;
        }
        // api/weather/Dublin or api/weather?city=Dublin
        public Weather GetWeatherForCity(string city)
        {
            Weather cityweather = forecasts.FirstOrDefault(w => w.City.ToUpper() == city.ToUpper());
            if(cityweather == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return cityweather;
        }

        public IEnumerable<string> GetCitiesWithWarning(bool warning)
        {
            var cities = forecasts.Where(w => w.Warning == warning).Select(w => w.City);
            return cities;
        }


        // POST: Forecast
        public IHttpActionResult PostForecast(Weather weather)
        {
            if (ModelState.IsValid)
            {
                lock (forecasts)
                {
                    // check for duplicate
                    var forecast = forecasts.SingleOrDefault(f => f.City.ToUpper() == weather.City.ToUpper());
                    if(forecast == null)
                    {
                        forecasts.Add(weather);

                        string uri = Request.RequestUri.ToString() + "id/" + weather.City;
                        return Created(uri, weather);
                    }
                    else
                    {
                        return BadRequest("resource already exsists");
                    }
                }
            }
            else
            {
                return BadRequest();
            }
        }

        public IHttpActionResult DeleteListing(string city)
        {
            lock (forecasts)
            {
                var record = forecasts.SingleOrDefault(f => f.City.ToUpper() == city.ToUpper());
                if(record != null)
                {
                    forecasts.Remove(record);
                    return Ok(forecasts.OrderBy(f => f.City).ToList());
                }
                else
                {
                    return NotFound();
                }
            }
        }


        public void UpdateCityWeather(string city, Weather weather)
        {
            if (ModelState.IsValid)
            {
                if(city == weather.City)
                {
                    int index = forecasts.FindIndex(w => weather.City.ToUpper() == weather.City.ToUpper());
                    if(index == -1)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        forecasts.RemoveAt(index);
                        forecasts.Add(weather);
                    }
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

    }
}