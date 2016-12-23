using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChallengeMeServer.Controllers
{
    public class SignUpController : ApiController
    {
        // GET: api/SignUp
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SignUp/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SignUp
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SignUp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SignUp/5
        public void Delete(int id)
        {
        }
    }
}
