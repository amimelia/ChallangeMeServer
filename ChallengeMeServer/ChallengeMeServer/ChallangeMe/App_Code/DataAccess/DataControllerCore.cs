using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ChallengeMeServer.Clients;
using ChallengeMeServer.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace ChallengeMeServer.ChallangeMe.App_Code.DataAccess
{
    public class DataControllerCore
    {
        public static DataControllerCore Current { get; } = new DataControllerCore();
        private static List<PostCommentInfo> _fillPostCommentList(List<post_comments> list)
        {
            var l = new List<PostCommentInfo>();
            list.ForEach(x =>
            {
                l.Add(new PostCommentInfo
                {
                    PostCommentContent = x.PostCommentContent,
                    PostCommentDiscription = x.PostCommentDiscription,
                    PostCommentLike = x.PostCommentLike
                });
            });
            return l;
        }

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
                db.user_followers.Remove(
                    db.user_followers.FirstOrDefault(x => x.UserID == userId && x.UserFollowerID == followerId));
                db.SaveChanges();
            }
        }

        public FeedInfo GetPostsForUser(int targetUser)
        {
            var posts = new List<PostInfo>();
            using (var db = new ChallengeMeEntities())
            {
                db.posts.ToList().ForEach(x=>
                {
                    if (x.UserID == targetUser)
                    {
                        posts.Add(new PostInfo
                        {
                            PostContent = x.PostContent,
                            PostDiscription = x.PostContent,
                            PostLikes = x.PostLikes,
                            PostCreateDate = x.PostCreateDate,
                            PostComments = _fillPostCommentList(x.post_comments.ToList())
                        });
                    }
                });
            }
            //posts.Sort();
            //ToDo dasortva unda datetimeis mixedvit
            return new FeedInfo
            {
                Posts = posts
            };
        }
    }
}