using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumClient
{
    public class Forum
    {
        public int ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public UserPost UserPost { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + " Subject: " + UserPost.Subject + " Message: " + UserPost.Message + " Timestamp: " + TimeStamp.ToString("dd/MM/yy H:mm:ss zzz");
        }
    }

    public class UserPost
    {
        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
