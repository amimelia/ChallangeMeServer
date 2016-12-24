using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ChallengeMeServer.Controllers.Web
{
    public class ChallangeMeException
    {
        Exception _challangeMeException;

        public ChallangeMeException(string errorMsg):this(new Exception(errorMsg))
        {
            
        }

        public ChallangeMeException(Exception errorEx)
        {
            _challangeMeException = errorEx;
        }

        /// <summary>
        /// to return exception as an HttpResponseMessage
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public HttpResponseMessage GetExceptionAsResponce(HttpRequestMessage Request)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, _challangeMeException);
        }

        /// <summary>
        /// To throw exception from code 
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public HttpResponseException GetException(HttpRequestMessage Request)
        {
            return new HttpResponseException(GetExceptionAsResponce(Request));
        }
    }
}