using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChallengeMeServer.ChallangeMe.App_Code.Managers;
using ChallengeMeServer.Controllers.Web;
using ChallengeMeServer.Managers;
using ChallengeMeServer.Models;

namespace ChallengeMeServer.Controllers
{
    public class NewsFeedController : CommonApiController
    {

        [ActionName("GetUserTimeLine")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public TimeLineInfo GetUserTimeLine(Guid tokenKey, int targetUserId)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);

            if (validationResponse != ValidRequest)
            {
                throw new ChallangeMeException("invalid.access.token").GetException(Request);
            }
            TimeLineInfo userTimeLine = null;
            try
            {
                userTimeLine = NewsFeedManager.Current.GetUserTimeLine(challangeMeRequest.Client, targetUserId);
            }
            catch (Exception ex)
            {
                throw new ChallangeMeException(ex).GetException(Request);
            }
            return userTimeLine;
        }

        [ActionName("GetUserInfo")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public UserInfo GetUserInfo(Guid tokenKey, int targetUserId)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);
            if (validationResponse != ValidRequest)
            {
                throw new ChallangeMeException("invalid.access.token").GetException(Request);
            }
            return NewsFeedManager.Current.GetUserInfo(challangeMeRequest.Client, targetUserId);
        }

        [ActionName("GetUserNewsFeedInfo")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public NewsFeedInfo GetUserNewsFeedInfo(Guid tokenKey)
        {
            var challangeMeRequest = new ChallengeMeRequest(tokenKey, null);
            var validationResponse = ValidateRequest(challangeMeRequest);
            if (validationResponse != ValidRequest)
            {
                throw new ChallangeMeException("invalid.access.token").GetException(Request);
            }
            return NewsFeedManager.Current.GetUserNewsFeedInfo(challangeMeRequest.Client);
        }
    }
}
