using ChallengeMeServer.Clients;
using ChallengeMeServer.Controllers.Web;
using ChallengeMeServer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ChallengeMeServer.Controllers
{
    public abstract class CommonApiController : ApiController
    {
        public static HttpResponseMessage ValidRequest = new HttpResponseMessage();

        public HttpResponseMessage ValidateRequest(ChallengeMeRequest request)
        {
            Client user = AccountManager.Current.GetClientByToken(request.TokenKey);
            if (user == AccountManager.InvalidClientToken || 
                user.IpAddress != HttpRequestHelper.GetClientIpString(this.Request))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            request.Client = user;

            if (!request.HasChallangeId())
                return ValidRequest;

            //TODO check challange id from challange manager

            return ValidRequest;
        }
    }
}
