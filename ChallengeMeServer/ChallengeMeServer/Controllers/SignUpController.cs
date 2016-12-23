using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChallengeMeServer.Controllers.Web;

namespace ChallengeMeServer.Controllers
{
    public class SignUpController : CommonApiController
    {
        // GET: api/SignUp
        [ActionName("SignIn")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid SignIn(String userName, String password)
        {
            return Guid.NewGuid();
        }
    }
}
