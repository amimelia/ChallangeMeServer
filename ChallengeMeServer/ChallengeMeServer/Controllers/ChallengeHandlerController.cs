using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class ChallengeHandlerController : ApiController
    {
        // GET: api/ChallengeHandler
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ChallengeHandler/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChallengeHandler
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChallengeHandler/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChallengeHandler/5
        public void Delete(int id)
        {
        }
    }
}
