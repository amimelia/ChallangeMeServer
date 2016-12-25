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
            var client = AccountManager.Current.GetClientByToken(request.TokenKey);
            if (client == AccountManager.InvalidClientToken || 
                client.IpAddress != HttpRequestHelper.GetClientIpString(Request))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            request.Client = client;
            if (!request.HasChallangeId())
                return ValidRequest;

            //TODO check challange id from challange manager

            return ValidRequest;
        }
    }
}
