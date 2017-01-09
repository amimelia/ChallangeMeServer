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
        #region Locks
        private Object _lockFacebookIdTable = new Object();
        private Object _lockUserFollowersTable = new Object();
        private Object s = new Object();
        #endregion

        #region StaticConstants
        private static int _invalidId = -1;
        #endregion

        public static DataControllerCore Current { get; } = new DataControllerCore();

        public List<user> GetUsers()
        {
            using (var db = new ChallengeMeEntities())
            {
                return db.users.ToList();
            }
        }

        public user GetUser(int userId)
        {
            user user;
            using (var db = new ChallengeMeEntities())
            {
                user = db.users.FirstOrDefault(x => x.UserID == userId);
            }
            return user;
        }
        public user GetUser(String email, String password)
        {
            user user;
            using (var db = new ChallengeMeEntities())
            {
                user = db.users.SingleOrDefault(x => x.Email == email && x.UserPassword == password);
            }
            return user;
        }

        public user GetUserByFacebookId(int facebookId)
        {
            user user;
            using (var db = new ChallengeMeEntities())
            {
                user = db.facebook_ids.FirstOrDefault(x => x.FacebookID == facebookId)?.user;
            }
            return user;
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

        public int AddNewUser(String email, String password, String fullName, String firstName, String lastName, DateTime birthDate, String gender, String pictureUrl)
        {
            int userId = _invalidId;
            using (var db = new ChallengeMeEntities())
            {
                if (!db.users.Any(x => x.Email.Equals(email)))
                {
                    user newUser = new user
                    {
                        Email = email,
                        UserPassword = password,
                        UserCreateDate = DateTime.Now,
                        UserStatus = "Active" // ar vicit ras shveba
                    };
                    db.users.Add(newUser);
                    db.SaveChanges();
                    profile_info newProfile = new profile_info
                    {
                        Name = firstName,
                        LastName = lastName,
                        BirthDate = birthDate,
                        Gender = gender,
                        UserID = newUser.UserID,
                        FullName = fullName,
                        ProfilePicture = pictureUrl
                    };
                    userId = newUser.UserID;
                    newUser.profile_info = newProfile;
                    db.SaveChanges();
                }
            }
            return userId;
        }


        public void AddFacebookId(int userId, long facebookId)
        {
            using (var db = new ChallengeMeEntities())
            {
                lock (_lockFacebookIdTable)
                {
                    db.facebook_ids.Add(new facebook_ids
                    {
                        FacebookID = facebookId,
                        UserID = userId,
                    });
                    db.SaveChanges();
                }
            }
        }

        public void AddFacebookSignUpInfo()
        {

        }


        public void AddUserFollower(int followerId, int userId)
        {
            using (var db = new ChallengeMeEntities())
            {
                lock (_lockUserFollowersTable)
                {
                    db.user_followers.Add(new user_followers
                    {
                        UserID = userId,
                        UserFollowerID = followerId
                    });
                    db.SaveChanges();
                }
            }
        }

        public void RemoveUserFollower(int followerId, int userId)
        {
            using (var db = new ChallengeMeEntities())
            {
                lock (_lockUserFollowersTable)
                {
                    var userToUnfollow =
                        db.user_followers.FirstOrDefault(x => x.UserID == userId && x.UserFollowerID == followerId);
                    if (userToUnfollow != null) db.user_followers.Remove(userToUnfollow);
                    db.SaveChanges();
                }
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