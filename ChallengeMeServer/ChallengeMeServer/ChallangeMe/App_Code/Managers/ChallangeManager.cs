using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.ChallangeMe.App_Code.Managers
{
    public class ChallangeManager
    {
        public enum ChallangeStatus : int
        {
            OpenChallange, ClosedChallange
        }

        public enum ChallangeType : int
        {
            PublicChallange, SpecificChallange
        }

        public int CreateChallange(Client user, string challangeName, string challangeHashTag, ChallangeType challangeType, ChallangeStatus challangeStatus)
        {
            return 1;
        }

        private bool ChallangeExists(string challangeName, string challangeHashTag)
        {
            return true;
        }

        private bool ValidChallangeInfo(string challangeName, string challangeHashTag)
        {
            return true;
        }
    }
}