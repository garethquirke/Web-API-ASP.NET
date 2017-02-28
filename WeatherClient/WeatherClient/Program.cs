using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherClient
{
    class Program
    {
        static string address = "http://localhost:51811/";
        /*
         * GET /api/weather                  get weather information for all cities       GetAllWeatherInformation()
         * GET /api/weather/city             get weather information for city             GetWeatherInformationForCity(city)
         * GET /api/weather?warning=true     get cities which have a weather warning      GetCitiesForWarningStatus(true)
         * PUT /api/weather                  update weather information for city          PutWeatherInformationForCity(city)
         */

        static async Task GetAllWeather()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Retrieve all Forecasts \n");

                    HttpResponseMessage response = await client.GetAsync("/api/weather/");
                    if (response.IsSuccessStatusCode)
                    {
                        var posts = await response.Content.ReadAsAsync<ICollection<Weather>>();
                        foreach (var p in posts)
                        {
                            Console.WriteLine("City: " + p.City + "\nTemperature: " + p.Temperature + "\nWeather Condition: " + p.Conditions
                                             + "\nWind Speed: " + p.WindSpeed + "\nWarning :" + p.WeatherWarning + "\n");
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

        static async Task GetCityWeather()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Retrieve forecast for a city\n");

                    HttpResponseMessage response = await client.GetAsync("/api/weather/Dublin");

                    // example get where the city is Galway
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        var post = await response.Content.ReadAsAsync<IList<Weather>>();
                        var p = post[0];
                        Console.WriteLine("City: " + p.City + "\nTemperature: " + p.Temperature + "\nWeather Condition: " + p.Conditions
                                             + "\nWind Speed: " + p.WindSpeed + "\nWarning :" + p.WeatherWarning + "\n");

                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Add a post
        static async Task AddAForecast()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Add a new forecast\n");

                    Weather weatherUpdate = new Weather() { City = "Tunisia", Temperature = 32, Conditions = "Sun is shining baby - costa del crumlin", WindSpeed = 1, WeatherWarning = true };
                    HttpResponseMessage response = await client.PostAsJsonAsync("/api/weather/", weatherUpdate);

                    if (response.IsSuccessStatusCode)
                    {
                        Uri postURI = response.Headers.Location;
                        var newCity = await response.Content.ReadAsAsync<Weather>();

                        Console.WriteLine("URI for new resource: " + postURI.ToString());
                        Console.WriteLine("City: " + newCity.City + "\nTemperature: " + newCity.Temperature + "\nWeather Condition: " + newCity.Conditions
                                             + "\nWind Speed: " + newCity.WindSpeed + "\nWarning :" + newCity.WeatherWarning + "\n");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        // Delete a city
        static async Task DeleteForecast()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Delete a city's forecast\n");

                    HttpResponseMessage response = await client.DeleteAsync("api/weather/DeleteListing/Limerick");
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else
                    {
                        Console.WriteLine("Deleted the record");
                        GetAllWeather().Wait();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Update a forecast
        static async Task UpdateForecast()
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    Console.WriteLine("Update a forecast\n");

                    Weather forecast = new Weather() { City = "Dublin", Temperature = 24, Conditions = "Sunny", WindSpeed = 5, WeatherWarning = true };
                    // Update the warning to false
                    forecast.WeatherWarning = false;
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    // Make the request to our API
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/weather/update/Dublin", forecast);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else
                    {
                        // success - output updated city
                        var updatedCity = await response.Content.ReadAsAsync<Weather>();
                        Console.WriteLine(updatedCity.City + " has been updated");
                    }
                }


            }
            catch (Exception e)
            {

            }
        }



        static void Main(string[] args)
        {
            GetAllWeather().Wait();
            GetCityWeather().Wait();

            AddAForecast().Wait();
            DeleteForecast().Wait();

            Console.ReadLine();
        }
    }
}
