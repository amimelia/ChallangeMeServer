using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ChallengeMeServer.Clients;
using ChallengeMeServer.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace ChallengeMeServer.ChallengeMe.App_Code.DataAccess
{
    public class DataControllerCore
    {
        public static DataControllerCore Current { get; } = new DataControllerCore();
       

        private Object _lockObject = new Object();

        public List<user> GetUsers()
        {
            using (var db = new ChallengeMeEntities())
            {
                return db.users.ToList();
            }
        }

        public user GetUser(int userId)
        {
            using (var db = new ChallengeMeEntities())
            {
                return db.users.FirstOrDefault(x => x.UserID == userId);
            }
        }
        public user GetUser(String userName, String password)
        {
            using (var db = new ChallengeMeEntities())
            {
                return db.users.SingleOrDefault(x => x.UserName == userName && x.UserPassword == password);
            }
        }

        //dasatestia
        public List<profile_info> GetSearchResults(string searchRequest)
        {
            // TODO: top ramdenime
            List<profile_info> listOfRelevantUsers;
            using (var db = new ChallengeMeEntities())
            {
                listOfRelevantUsers = db.profile_info.Where(userProfile => userProfile.Name.Contains(searchRequest)
                    || userProfile.LastName.Contains(searchRequest)).ToList();
            }
            return listOfRelevantUsers;
        }


        public ProfileInfo GetProfileInfo(int targetUserID)
        {
            ProfileInfo profileInfo;
            using (var db = new ChallengeMeEntities())
            {
                var profile_info = db.users.ToList().SingleOrDefault(user => user.UserID == targetUserID).profile_info;
                profileInfo = new ProfileInfo(profile_info);
            }
            return profileInfo;
        }

        public void RegisterNewUser()
        {

        }


        public void AddUserFollower(int followerId, int userId)
        {
            using (var db = new ChallengeMeEntities())
            {
                db.user_followers.Add(new user_followers
                {
                    UserID = userId,
                    UserFollowerID = followerId
                });
                db.SaveChanges();
            }
        }

        public void RemoveUserFollower(int followerId, int userId)
        {
            using (var db = new ChallengeMeEntities())
            {
                var userToUnfollow = db.user_followers.FirstOrDefault(x => x.UserID == userId && x.UserFollowerID == followerId);
                if (userToUnfollow != null) db.user_followers.Remove(userToUnfollow);
                db.SaveChanges();
            }
        }

        public FeedInfo GetPostsForUser(int targetUser)
        {
            FeedInfo feedInfo;
            using (var db = new ChallengeMeEntities())
            {
                var posts = db.posts.Where(post => post.UserID == targetUser).ToList();
                feedInfo = new FeedInfo(posts);
            }
            return feedInfo;
        }
    }
}