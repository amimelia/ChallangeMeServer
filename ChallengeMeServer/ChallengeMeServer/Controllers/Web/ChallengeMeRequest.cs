using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Controllers.Web
{
    public class ChallengeMeRequest
    {
        public ChallengeMeRequest(Guid token, int? challangeId)
        {
            TokenKey = token;
        }

        public int? ChallangeId { get; set; }
        public Guid TokenKey { get; set; }
        public bool IsValid { get; set; }
        
        public bool HasChallangeId()
        {
            return (ChallangeId != null && ChallangeId > 0);
        }

        public Client Client { get; set; }       
    }
}