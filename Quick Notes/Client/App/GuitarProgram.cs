using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GuitarClient
{
    class Program
    {
        static string address = "http://localhost:53905/";

        /* /guitar/all 
         * /guitar/name/Mustang 
         * /guitar/new
         * (POST) /guitar/GUITAR
         */

        static async Task GetAllGuitars()
        {
            Console.WriteLine("Get all guitars in the inventory \n");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Retrieve all Guitars in stock \n");

                    HttpResponseMessage response = await client.GetAsync("/guitar/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var guitars = await response.Content.ReadAsAsync<ICollection<Guitar>>();
                        foreach (var g in guitars)
                        {
                            Console.WriteLine("ID: " + g.ID + " Name: " + g.Name + " Make: " + g.Make + " Is new?: " + g.IsNew + " Stock: " + g.Stock);
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


        static async Task GetGuitarByName()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Get Guitar by name: Mustang \n");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("guitar/name/Mustang");

                    try
                    {
                        response.EnsureSuccessStatusCode();
                        var guitar = await response.Content.ReadAsAsync<Guitar>();
                        Console.WriteLine("ID: " + guitar.ID + " Name: " + guitar.Name + " Make: " + guitar.Make + " Is new?: " + guitar.IsNew + " Stock: " + guitar.Stock + "\n");
                    }
                    catch(HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetNewGuitars()
        {
            Console.WriteLine("Get all the new guitars in the inventory \n");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);

                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));           

                    HttpResponseMessage response = await client.GetAsync("/guitar/new");                          
                    if (response.IsSuccessStatusCode)                                                   
                    {
                        var guitars = await response.Content.ReadAsAsync<IEnumerable<Guitar>>();

                        foreach (var g in guitars)
                        {
                            Console.WriteLine("ID: " + g.ID + " Name: " + g.Name + " Make: " + g.Make + " Is new?: " + g.IsNew + " Stock: " + g.Stock);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }


        static async Task AddAGuitar()
        {
            Console.WriteLine("Add a new guitar, a nice martin too \n");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.
                      Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    Guitar guitar = new Guitar() { ID = 7, Name = "ooo-18", Make = "Martin", Stock = 1, IsNew = true };

                    HttpResponseMessage response = await client.PostAsJsonAsync("guitar", guitar);

                    if (response.IsSuccessStatusCode)
                    {
                        Uri postUri = response.Headers.Location;
                        var newGuitar = await response.Content.ReadAsAsync<Guitar>();

                        Console.WriteLine("URI for new resource: " + postUri.ToString());
                        Console.WriteLine("ID: " + newGuitar.ID + " Name: " + newGuitar.Name + " Make: " + newGuitar.Make + " Is new?: " + newGuitar.IsNew + " Stock: " + newGuitar.Stock);

                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }


        static void Main(string[] args)
        {
            GetAllGuitars().Wait();
            GetGuitarByName().Wait();
            GetNewGuitars().Wait();
            AddAGuitar().Wait();
            Console.ReadLine();
        }
    }
}
