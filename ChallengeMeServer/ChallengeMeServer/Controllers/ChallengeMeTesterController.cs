using ChallengeMeServer.Controllers.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class ChallengeMeTesterController : CommonApiController
    {
        // GET: api/ChallengeMeTester
        public IEnumerable<string> Get()
        {

            return new string[] { "amirani", "ako" };
        }

        // GET: api/ChallengeMeTester/5
        public string Get(int id)
        {
            return "";
        }

        [ActionName("test")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string PostChallangeMeRequest(Guid tokenKey, int challangeId)
        {
            var challangeMeRequest = new ChallangeMeRequest(tokenKey, challangeId);
            var validationResponse = ValidateRequest(challangeMeRequest);
            if (validationResponse != CommonApiController.Valid_Request)
                return "invalid";
            return "success";
        }

        // POST: api/ChallengeMeTester
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChallengeMeTester/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChallengeMeTester/5
        public void Delete(int id)
        {
        }
    }


    
}
