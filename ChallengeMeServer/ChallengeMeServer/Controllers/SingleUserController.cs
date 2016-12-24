using ChallengeMeServer.ChallangeMe.App_Code.Managers;
using ChallengeMeServer.Clients;
using ChallengeMeServer.Controllers.Web;
using ChallengeMeServer.Managers;
using ChallengeMeServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class SingleUserController : CommonApiController
    {
        
        [ActionName("FollowUser")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FollowUser(Guid tokenKey, int userToFollowId)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);

            if (validationResponse != ValidRequest)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            }

            AccountManager.Current.AddUserFollower(challangeMeRequest.Client, userToFollowId);

            NotificationsManager.Current.AddUserFollowNotification(challangeMeRequest.Client, userToFollowId);

            return null;
        }

        [ActionName("UnFollowUser")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage UnFollowUser(Guid tokenKey, int userToFollowId)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);

            if (validationResponse != ValidRequest)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            }

            AccountManager.Current.RemoveUserFollower(challangeMeRequest.Client, userToFollowId);

            return null;
        }

        [ActionName("GetUserPosts")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public FeedInfo GetUserPosts(Guid tokenKey, int targetUser)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);

            if (validationResponse != ValidRequest)
            {
                throw new ChallangeMeException("invalid.access.token").GetException(Request);
            }

            FeedInfo postsForUser = null;

            try
            {
                postsForUser = NewsFeedManager.Current.GetPostsForUser(challangeMeRequest.Client, targetUser);
            }
            catch (Exception ex)
            {
                throw new ChallangeMeException(ex).GetException(Request);
            }

            return postsForUser;
        }

        [ActionName("GetUserInfo")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public UserInfo GetUserInfo(Guid tokenKey, int targetUser)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);

            if (validationResponse != ValidRequest)
            {
                throw new ChallangeMeException("invalid.access.token").GetException(Request);
            }

            Client targetClient = null;

            try
            {
                targetClient = AccountManager.Current.GetInfoForUser(challangeMeRequest.Client, targetUser);
            }
            catch (Exception ex)
            {
                throw new ChallangeMeException(ex).GetException(Request);
            }

            UserInfo infoForUser = new UserInfo(targetClient);  
            return infoForUser;
        }

        [ActionName("UpdateUserInfo")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage UpdateUserInfo(Guid tokenKey, string userName, string userFirstName, string userLastName)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);

            if (validationResponse != ValidRequest)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            }

            try
            {
                AccountManager.Current.UpdateUserBasicInfo(challangeMeRequest.Client, userName, userFirstName, userLastName);
            } catch(Exception ex)
            {
                return new ChallangeMeException(ex).GetExceptionAsResponce(Request);
            }
            
            return null;
        }

    }
}
