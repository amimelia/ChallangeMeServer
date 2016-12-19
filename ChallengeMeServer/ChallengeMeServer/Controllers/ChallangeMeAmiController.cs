using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class ChallangeMeAmiController : ApiController
    {
        // GET: api/ChallangeMeAmi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ChallangeMeAmi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChallangeMeAmi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChallangeMeAmi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChallangeMeAmi/5
        public void Delete(int id)
        {
        }
    }
}
