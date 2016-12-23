using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class NewsFeedController : ApiController
    {
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
