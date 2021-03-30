using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;


namespace TweetAPP.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetAppDBContext dbcontext;

        public TweetRepository(TweetAppDBContext context)
        {
            dbcontext = context;
        }

        public async Task<bool> ForgotPassword(string emailId, string password)
        {
            var result = await dbcontext.Users.Where(s => s.EmailId == emailId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Password = password;
                dbcontext.Update(result);
                var response = dbcontext.SaveChanges();
                if (response > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IList<Tweet>> GetAllTweets()
        {
            var result = await dbcontext.Tweets.ToListAsync();
            return result;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var result = await dbcontext.Users.ToListAsync();
            return result;
        }

        public async Task<IList<Tweet>> GetTweetsByUser(int userID)
        {
            var result = await dbcontext.Tweets.Where(i => i.UserId == userID).ToListAsync();
            return result;
        }

        public async Task<bool> Login(string emailId, string password)
        {
            User user = await dbcontext.Users.SingleOrDefaultAsync(e => e.EmailId == emailId && e.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> PostTweet(PostTweet tweet)
        {
            Tweet tweets = new Tweet();
            if (tweet != null)
            {
                tweets.Id = tweet.Id;
                tweets.UserId = tweet.UserId;
                tweets.Tweeets = tweet.Tweeets;
            }
            dbcontext.Tweets.Add(tweets);
            var result = await dbcontext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Register(User users)
        {
            dbcontext.Users.Add(users);
            var result = await dbcontext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            var update = await dbcontext.Users.Where(x => x.EmailId == emailId && x.Password == oldpassword).FirstOrDefaultAsync();
            if (update != null)
            {
                update.Password = newPassword;
                dbcontext.Users.Update(update);
                var result = await dbcontext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
            }



            return false;
        }

        public async Task<User> ValidateEmailId(string emailId)
        {
            var user = await dbcontext.Users.FirstOrDefaultAsync(e => e.EmailId == emailId);
            return user;

        }
    }
}
