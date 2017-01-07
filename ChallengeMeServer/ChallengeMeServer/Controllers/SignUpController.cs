using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChallengeMeServer.Controllers.Web;
using ChallengeMeServer.Managers;

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
            return AccountManager.Current.CheckSignInValidation(userName, password, Request);
        }

        [ActionName("SignUp")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid SignUp(String token, String facebookId)
        {
            return new Guid();
        }
    }
}
