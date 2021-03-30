using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Entities;

namespace TweetAPP.Service
{
    public interface ITweetAppService
    {
        Task<string> UserRegister(User users);

        Task<string> UserLogin(string emailId, string password);

        Task<IList<Tweet>> GetAllTweets();

        Task<IList<Tweet>> GetTweetsByUser(int userID);

        Task<IList<User>> GetAllUsers();

        Task<string> PostTweet(PostTweet tweet);

        Task<string> UpdatePassword(string emailId, string oldpassword, string newPassword);
        Task<string> ForgotPassword(string emailId, string password);

    }
}
