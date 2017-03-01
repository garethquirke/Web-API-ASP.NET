using ForumAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ForumAPI.Controllers
{
    [RoutePrefix("forum")]
    public class ForumController : ApiController
    {
        static private List<Forum> posts = new List<Forum>()
        {
            new Forum {ID = 0, TimeStamp = DateTime.Now, UserPost = new UserPost { Subject = "Rome", Message = "Who's coming to Rome" } },
            new Forum {ID = 1, TimeStamp = DateTime.Now, UserPost = new UserPost { Subject = "Guitars", Message = "Sit down I'll play u a song" } },
            new Forum {ID = 2, TimeStamp = DateTime.Now, UserPost = new UserPost { Subject = "Dominos", Message = "pizza not bad aye" } },
            new Forum {ID = 3, TimeStamp = DateTime.Now, UserPost = new UserPost { Subject = "Sushi", Message = "lovely japanese food" } },
            new Forum {ID = 4, TimeStamp = DateTime.Now, UserPost = new UserPost { Subject = "Brian Potter", Message = "kenny get me a zantac" } },

        };
        

        [Route("all")]
        public IHttpActionResult GetAllPosts()
        {
            lock (posts)
            {
                return Ok(posts);
            }
        }



        [Route("last/{count:min(1)}")]
        public IHttpActionResult GetLastFive(int count)
        {
            lock (posts)
            {
                var recent = posts.OrderByDescending(p => p.ID).Take(count);
                return Ok(recent.ToList());
            }
        }




        [Route("id/{id:int}")]
        public IHttpActionResult GetPostByID(int id)
        {
            lock (posts)
            {
                Forum post = posts.FirstOrDefault(p => p.ID == id);
                if(post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
        }


        [Route("")]
        public IHttpActionResult AddAPost(UserPost post)
        {
            if (ModelState.IsValid)
            {
                Forum f;
                lock (posts)
                {
                    int id;
                    if(posts.Count == 0)
                    {
                        id = 0;
                    }
                    else
                    {
                        id = posts[posts.Count - 1].ID + 1;
                    }

                    f = new Forum
                    {
                        ID = id,
                        TimeStamp = DateTime.Now,
                        UserPost = post
                    };
                    posts.Add(f);
                }
                string uri = Request.RequestUri.ToString() + "/id/" + f.ID;
                return Created(uri, f);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
