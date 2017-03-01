using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ForumClient
{
    class Program
    {
        static string address = "http://localhost:62022";


        static async Task GetAllPosts()
        {
            Console.WriteLine("Retrieve all posts");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);

                    client.DefaultRequestHeaders.
                       Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("/forum/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var posts = await response.Content.ReadAsAsync<IEnumerable<Forum>>();
                        foreach (var p in posts)
                        {
                            Console.WriteLine(p);
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


        static async Task GetPostByID()
        {
            Console.WriteLine("Retrieve all posts by ID");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);

                    client.DefaultRequestHeaders.
                       Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("forum/id/2");

                    if (response.IsSuccessStatusCode)
                    {
                        var post = await response.Content.ReadAsAsync<Forum>();
                        Console.WriteLine(post);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
        }



        static async Task AddPost()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(address);
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    UserPost post = new UserPost()
                    {
                        Subject = "This post has been added aint it",
                        Message = "Wow lots of posts"
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync("forum", post);

                    if (response.IsSuccessStatusCode)
                    {
                        Uri postUri = response.Headers.Location;
                        var forum = await response.Content.ReadAsAsync<Forum>();
                        Console.WriteLine("URI for new resource: " + postUri.ToString());
                        Console.WriteLine(forum);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main(String[] args)
        {
            GetAllPosts().Wait();
            GetPostByID().Wait();
            AddPost().Wait();
            Console.ReadLine();
        }
    }
}