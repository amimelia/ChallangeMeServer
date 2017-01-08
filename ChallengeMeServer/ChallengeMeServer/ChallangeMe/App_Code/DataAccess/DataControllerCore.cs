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
            // TODO: top 10 cali wamovige
            List<profile_info> listOfRelevantUsers;
            using (var db = new ChallengeMeEntities())
            {
                listOfRelevantUsers = db.profile_info.Where(userProfile => userProfile.Name.Contains(searchRequest)
                    || userProfile.LastName.Contains(searchRequest)).ToList();
            }
            return listOfRelevantUsers.Take(10).ToList();
        }


        public profile_info GetProfile(int targetUserId)
        {

            profile_info profileInfo;
            using (var db = new ChallengeMeEntities())
            {
                profileInfo = db.users.ToList().SingleOrDefault(user => user.UserID == targetUserId)?.profile_info;
            }
            return profileInfo;
        }

        public int AddNewUser(String email, String password,String fullName, String name, String lastName, DateTime birthDate, String gender)
        {
            int userId;
            using (var db = new ChallengeMeEntities())
            {
                user newUser = new user
                {
                    Email = email,
                    UserPassword = password,
                    UserCreateDate = DateTime.Now,
                    UserStatus = "Active"  // ar vicit ras shveba
                };
                db.users.Add(newUser);
                db.SaveChanges();
                profile_info newProfile = new profile_info
                {
                    Name = name,
                    LastName = lastName,
                    BirthDate = birthDate,
                    Gender = gender,
                    UserID = newUser.UserID,
                    FullName = fullName
                };
                userId = newUser.UserID;
                newUser.profile_info = newProfile;
                db.SaveChanges();
            }
            return userId;
        }


        public void AddFacebookId(int userId, int facebookId)
        {
            using (var db = new ChallengeMeEntities())
            {
                db.facebook_ids.Add(new facebook_ids
                {
                    FacebookID = facebookId,
                    UserID = userId,
                });
                db.SaveChanges();
            }
        }

        public void AddFacebookSignUpInfo()
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

        public List<post> GetPostsForUser(int targetUser)
        {
            List<post> posts;
            using (var db = new ChallengeMeEntities())
            {
                posts = db.posts.Where(post => post.UserID == targetUser).ToList();
            }
            return posts;
        }

        public post GetPostById(int targetPostId)
        {
            post post;
            using (var db = new ChallengeMeEntities())
            {
                post = db.posts.SingleOrDefault(x => x.PostID == targetPostId);
            }
            return post;
        }

        public void SetLikeToPost(int targetPostId)
        {
            using (var db = new ChallengeMeEntities())
            {
                var postToLike = db.posts.SingleOrDefault(post => post.PostID == targetPostId);
                if (postToLike != null) postToLike.PostLikes += 1;
                db.SaveChanges();
            }
        }

        public void SetLikeToComment(int targetCommentId)
        {
            using (var db = new ChallengeMeEntities())
            {
                var commentToLike = db.post_comments.SingleOrDefault(comment => comment.PostCommentID == targetCommentId);
                if (commentToLike != null) commentToLike.PostCommentLike += 1;
                db.SaveChanges();
            }
        }

        public void CreatePostComment(int clientUserId, int targetPostId, string postCommentContent, string postCommentDescription)
        {
            using (var db = new ChallengeMeEntities())
            {
                db.post_comments.Add(new post_comments
                {
                    PostCommentContent = postCommentContent,
                    PostCommentCreateDate = DateTime.Now,
                    PostCommentDiscription = postCommentDescription,
                    PostID = targetPostId,
                    UserID = clientUserId,
                    user = db.users.SingleOrDefault(user => user.UserID == clientUserId),
                    PostCommentLike = 0,
                    post = db.posts.SingleOrDefault(post => post.PostID == targetPostId)
                });
                db.SaveChanges();
            }
        }
    }
}