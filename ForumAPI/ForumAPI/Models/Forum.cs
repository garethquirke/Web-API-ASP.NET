using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ForumAPI.Models
{
    public class Forum
    {
        public int ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public UserPost UserPost { get; set; }
    }

    public class UserPost
    {

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}