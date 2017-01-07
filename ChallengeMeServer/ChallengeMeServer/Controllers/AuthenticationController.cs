using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChallengeMeServer.Controllers.Web;
using ChallengeMeServer.Managers;
using Facebook;

namespace ChallengeMeServer.Controllers
{
    public class AuthenticationController : CommonApiController
    {
        // GET: api/SignUp
        [ActionName("SignIn")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid SignIn(String userName, String password)
        {
            return AccountManager.Current.CheckSignInValidation(userName, password, Request);
        }

        [ActionName("FacebookSignUp")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid FacebookSignUp(String token, String facebookId)
        {
            return new Guid();
        }

        [ActionName("EmailSignUp")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public void EmailSignUp(String email, String password, String name, String lastName, DateTime birthDate, Boolean gender)
        {
            AccountManager.Current.EmailSignUp(email, password, name, lastName, birthDate, gender);
        }
    }
}
