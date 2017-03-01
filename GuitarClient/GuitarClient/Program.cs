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



        static async Task GetAllGuitars()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Retrieve all Guitars in stock \n");

                    HttpResponseMessage response = await client.GetAsync("/api/guitar/");
                    if (response.IsSuccessStatusCode)
                    {
                        var guitars = await response.Content.ReadAsAsync<ICollection<Guitar>>();
                        foreach (var g in guitars)
                        {
                            Console.WriteLine("ID: " + g.ID + "Name: " + g.Name + "Make: " + g.Make + "Is new?: " + g.IsNew + "Stock: " + g.Stock);
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
            GetAllGuitars().Wait();
            Console.ReadLine();
        }
    }
}
