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
        [ActionName("EmailSignIn")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid EmailSignIn(String email, String password)
        {
            return AccountManager.Current.CheckEmailSignInValidation(email, password, Request);
        }

        [ActionName("EmailSignUp")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid EmailSignUp(String email, String password, String fullName, String name, String lastName, DateTime birthDate, String gender)
        {
            return AccountManager.Current.EmailSignUp(email, password, fullName, name, lastName, birthDate, gender, Request);
        }

        [ActionName("FacebookAuthentication")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Guid FacebookAuthentication(String tokenKey, String facebookId)
        {
            return AccountManager.Current.CheckFacebookAuthenticationValidation(tokenKey,facebookId,Request);
        }
    }
}
