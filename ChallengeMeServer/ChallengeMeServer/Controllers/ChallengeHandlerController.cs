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



        // GET: api/SignUp
        [ActionName("CreateChallange")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid SignIn(String userName, String password)
        {
            return new Guid();
        }
    }
}
