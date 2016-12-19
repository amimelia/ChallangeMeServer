using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class ChallengeMeTesterController : ApiController
    {
        // GET: api/ChallengeMeTester
        public IEnumerable<string> Get()
        {
            return new string[] { "amirani", "ako" };
        }

        // GET: api/ChallengeMeTester/5
        public string Get(int id)
        {
            return "value";
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
