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

        [ActionName("GetUserPosts")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public FeedInfo GetUserPosts(Guid tokenKey, int targetUserId)
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
                postsForUser = NewsFeedManager.Current.GetUserPosts(challangeMeRequest.Client, targetUserId);
            }
            catch (Exception ex)
            {
                throw new ChallangeMeException(ex).GetException(Request);
            }
            return postsForUser;
        }

        // GET: api/NewsFeed
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/NewsFeed/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NewsFeed
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/NewsFeed/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NewsFeed/5
        public void Delete(int id)
        {
        }
    }
}
