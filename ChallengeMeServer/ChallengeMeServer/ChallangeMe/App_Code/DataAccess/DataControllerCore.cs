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
        private static List<PostCommentInfo> _fillPostCommentList(List<post_comments> list)
        {
            var postCommentInfo = new List<PostCommentInfo>();
            list.ForEach(x =>
            {
                postCommentInfo.Add(new PostCommentInfo
                {
                    PostCommentContent = x.PostCommentContent,
                    PostCommentDescription = x.PostCommentDiscription,
                    PostCommentLike = x.PostCommentLike
                });
            });
            return postCommentInfo;
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

        public ProfileInfo GetProfileInfo(int targetUserID)
        {
            ProfileInfo profileInfo;
            using (var db = new ChallengeMeEntities())
            {
                var profile_info = db.users.ToList().SingleOrDefault(user => user.UserID == targetUserID).profile_info;
                profileInfo = new ProfileInfo
                {
                    Name = profile_info.Name,
                    LastName = profile_info.LastName,
                    BirthDate = profile_info.BirthDate,
                    Gender = profile_info.Gender,
                    ProfilePicture = profile_info.ProfilePicture
                };
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
            var posts = new List<PostInfo>();
            using (var db = new ChallengeMeEntities())
            {
                db.posts.ToList().ForEach(x =>
                {
                    if (x.UserID == targetUser)
                    {
                        posts.Add(new PostInfo
                        {
                            PostContent = x.PostContent,
                            PostDescription = x.PostContent,
                            PostLikes = x.PostLikes,
                            PostCreateDate = x.PostCreateDate,
                            PostComments = _fillPostCommentList(x.post_comments.ToList())
                        });
                    }
                });
            }
            posts = posts.OrderBy(p => p.PostCreateDate).ToList();
            return new FeedInfo
            {
                Posts = posts
            };
        }
    }
}