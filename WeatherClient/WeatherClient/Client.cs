// client for Weather Service 

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Weather.Models;

namespace WeatherClient
{
    class Client
    {
        // async call
        static async Task DoWork()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:42629/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // 1
                    // get weather info for all cities
                    // GET ../weather/all
                    HttpResponseMessage response = await client.GetAsync("weather/all");
                    if (response.IsSuccessStatusCode)                                                   // 200.299
                    {
                        // read result 
                        var weather = await response.Content.ReadAsAsync<IEnumerable<WeatherInformation>>();
                        foreach (var w in weather)
                        {
                            Console.WriteLine(w.City + " " + w.Temperature + "C " + w.Conditions + " warning: " + w.WeatherWarning);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // 2
                    // get weather info for Dublin
                    // GET ../weather/city/Dublin
                    WeatherInformation weatherInfo = new WeatherInformation();
                    response = await client.GetAsync("weather/city/Dublin");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        weatherInfo = await response.Content.ReadAsAsync<WeatherInformation>();
                        Console.WriteLine(weatherInfo.City + " " + weatherInfo.Temperature + "C " + weatherInfo.Conditions + " Warning: " + weatherInfo.WeatherWarning);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // 3
                    // get cities with weather warnings in place
                    // GET ../weather/cities/warning/true
                    response = client.GetAsync("weather/cities/warning/true").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var cities = await response.Content.ReadAsAsync<IEnumerable<String>>();
                        foreach (String city in cities)
                        {
                            Console.WriteLine(city);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // kick off
        static void Main()
        {
            DoWork().Wait();
        }
    }
}
