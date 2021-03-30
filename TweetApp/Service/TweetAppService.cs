using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Entities;
using TweetAPP.Repositories;

namespace TweetAPP.Service
{
    public class TweetAppService : ITweetAppService
    {
        private readonly ITweetRepository tweetRepository;
        private ILogger<TweetAppService> logger;

        public TweetAppService(ITweetRepository tweetRepository, ILogger<TweetAppService> logger)
        {
            this.tweetRepository = tweetRepository;
            this.logger = logger;
        }

        public async Task<string> ForgotPassword(string emailId, string password)
        {
            string message = string.Empty;
            password = this.EncryptPassword(password);
            var result = await this.tweetRepository.ForgotPassword(emailId, password);
            if (result)
            {
                message = "Changed Password";
            }
            else
            {
                message = "Failed";
            }
            return message;
        }

        public async Task<IList<Tweet>> GetAllTweets()
        {
            try
            {
                var result = await this.tweetRepository.GetAllTweets();
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }

        public async Task<IList<User>> GetAllUsers()
        {
            try
            {
                var result = await this.tweetRepository.GetAllUsers();
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all registered users");
                throw;
            }
        }

        public async Task<IList<Tweet>> GetTweetsByUser(int userID)
        {
            try
            {
                var result = await this.tweetRepository.GetTweetsByUser(userID);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }

        public async Task<string> PostTweet(PostTweet tweet)
        {
            try
            {
                string message = string.Empty;
                var result = await this.tweetRepository.PostTweet(tweet);
                if (result > 0)
                {
                    message = "Posted";
                }
                else
                {
                    message = "Error occured";
                }
                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }

        public async Task<string> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            try
            {
                string message = string.Empty;
                newPassword = this.EncryptPassword(newPassword);
                oldpassword = this.EncryptPassword(oldpassword);
                var result = await this.tweetRepository.UpdatePassword(emailId, oldpassword, newPassword);
                if (result)
                {
                    message = "Updated Successfully";
                }
                else
                {
                    message = "Update Failed";
                }
                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }

        public async Task<string> UserLogin(string emailId, string password)
        {
            try
            {
                string message = string.Empty;
                password = this.EncryptPassword(password);
                var result = await this.tweetRepository.Login(emailId, password);
                if (result)
                {
                    message = "Successfully logged in";
                }
                else
                {
                    message = "login failed";
                }
                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }

        public async Task<string> UserRegister(User users)
        {
            try
            {
                string message = string.Empty;
                var validate = await this.tweetRepository.ValidateEmailId(users.EmailId);
                if (validate == null)
                {
                    users.Password = this.EncryptPassword(users.Password);
                    var result = await this.tweetRepository.Register(users);
                    if (result > 0)
                    {
                        message = "Successfully registerd";
                    }
                    else
                    {
                        message = "Registration failed";
                    }
                }
                else
                {
                    message = "EmailId is already used";
                }

                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }

        private string EncryptPassword(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }
}