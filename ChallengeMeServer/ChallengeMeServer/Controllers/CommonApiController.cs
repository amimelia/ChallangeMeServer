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
        public static HttpResponseMessage Valid_Request = new HttpResponseMessage();

        public HttpResponseMessage ValidateRequest(ChallangeMeRequest request)
        {
            Client user = AccauntManager.Current.GetClientByToken(request.TokenKey);
            if (user == AccauntManager.INVALID_CLIENT_TOKEN || 
                user.IpAddress != HttpRequestHelper.GetClientIpString(this.Request))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            request.Client = user;

            if (!request.HasChallangeId())
                return Valid_Request;

            //TODO check challange id from challange manager

            return Valid_Request;
        }
    }
}
