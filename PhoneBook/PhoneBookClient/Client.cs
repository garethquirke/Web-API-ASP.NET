using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using EAD2PhoneBook.Models;


namespace PhoneBookClient
{
    class Client
    {
       static async Task DoWork()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage response = await client.GetAsync("api/phonebook");
                    if (response.IsSuccessStatusCode)
                    {
                        var contacts = await response.Content.ReadAsAsync<IEnumerable<PhoneBook>>();
                        foreach(var c in contacts)
                        {
                            Console.WriteLine(c.Name + " " + c.Address + " " + c.Number);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
