using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApp.Entities
{
    public partial class User
    {
        public User()
        {
            Tweets = new HashSet<Tweet>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }
    }
}
