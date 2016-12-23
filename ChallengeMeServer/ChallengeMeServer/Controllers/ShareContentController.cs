using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class ShareContentController : ApiController
    {
        // GET: api/ShareContent
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ShareContent/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ShareContent
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ShareContent/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ShareContent/5
        public void Delete(int id)
        {
        }
    }
}
