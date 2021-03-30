using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetAPP.Repositories
{
    public interface ITweetRepository
    {
        Task<int> Register(User users);

        Task<bool> Login(string emailId, string password);

        Task<IList<Tweet>> GetAllTweets();

        Task<IList<Tweet>> GetTweetsByUser(int userID);

        Task<IList<User>> GetAllUsers();

        Task<int> PostTweet(PostTweet tweet);

        Task<bool> UpdatePassword(string emailId, string oldpassword, string newPassword);

        Task<bool> ForgotPassword(string emailId, string password);

        Task<User> ValidateEmailId(string emailId);
    }
}
