using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CA1_CLIENT_X00108966
{
    class Program
    {
        static string address = "http://localhost:2133/";


        static async Task GetAllMovies()
        {
            Console.WriteLine("Retrieve all movies");

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri(address);

                    client.DefaultRequestHeaders.
                       Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("/movie/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var movies = await response.Content.ReadAsAsync<IEnumerable<Movie>>();
                        foreach (var m in movies)
                        {
                            Console.WriteLine(m);
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
                Console.WriteLine(e);
            }

            Console.WriteLine();
        }


        static async Task GetMovieByID()
        {
            Console.WriteLine("Retrieve movie by ID");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);

                    client.DefaultRequestHeaders.
                       Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("movie/id/2");

                    if (response.IsSuccessStatusCode)
                    {
                        var movie = await response.Content.ReadAsAsync<Movie>();
                        Console.WriteLine(movie);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
        }


        static async Task AddReview()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    Review review = new Review()
                    {
                        Author = "Tom Collins",
                        Text = "Wow so good, do another one please",
                        Rating = 8
                    };


                    HttpResponseMessage response = await client.PostAsJsonAsync("movie", review);

                    if (response.IsSuccessStatusCode)
                    {
                        Uri postUri = response.Headers.Location;
                        var movie = await response.Content.ReadAsAsync<Movie>();
                        Console.WriteLine("URI for new resource: " + postUri.ToString());
                        Console.WriteLine(movie);
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
            GetAllMovies().Wait();
            GetMovieByID().Wait();
            AddReview().Wait();
            Console.ReadLine();
        }
    }
}
