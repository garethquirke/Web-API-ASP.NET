using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherClient
{
    class Program
    {
        static async Task Run()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51811/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Retrieve all Forecasts \n");
                    // Retrieve all forecasts
                    HttpResponseMessage response = await client.GetAsync("api/weather");               
                    if (response.IsSuccessStatusCode)                                                  
                    {
                        // read result 
                        var weather = await response.Content.ReadAsAsync<IEnumerable<Weather>>();
                        foreach (var w in weather)
                        {
                            Console.WriteLine(w.City + " " + w.Temperature + "C " + w.WindSpeed + "km/h " + w.Conditions + " warning: " + w.WeatherWarning);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }



                    Console.WriteLine("Forecast for Dublin \n");
                    // 2
                    // get weather info for Dublin
                    // GET ../api/weather/Dublin
                    Weather weatherInfo = new Weather();
                    response = await client.GetAsync("api/weather/Dublin");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        weatherInfo = await response.Content.ReadAsAsync<Weather>();
                        Console.WriteLine(weatherInfo.City + " " + weatherInfo.Temperature + "C " + weatherInfo.WindSpeed + "km/h " + weatherInfo.Conditions + " Warning: " + weatherInfo.WeatherWarning);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }


                    Console.WriteLine("Update forecast for Dublin \n");
                    // 3
                    // update by Put to api/weather/Dublin - now its sunny
                    weatherInfo.Conditions = "Sunny";
                    response = await client.PutAsJsonAsync("api/weather/Dublin", weatherInfo);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }


                    Console.WriteLine("Retrieve the weather warnings in places\n");
                    // 4
                    // get cities with weather warnings in place
                    // GET ../api/weather?warning=true
                    response = await client.GetAsync("api/weather?warning=true");
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
        static void Main(string[] args)
        {
            Run().Wait();
            Console.ReadLine();
        }
    }
}
