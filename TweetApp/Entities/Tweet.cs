using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApp.Entities
{
    public partial class Tweet
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Tweeets { get; set; }

        public virtual User User { get; set; }
    }
}
