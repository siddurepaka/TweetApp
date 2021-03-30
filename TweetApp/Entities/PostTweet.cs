using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Entities
{
    public class PostTweet
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Tweeets { get; set; }
    }
}
