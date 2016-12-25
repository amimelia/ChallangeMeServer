using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChallengeMeServer.Clients;

namespace ChallengeMeServer.Models
{
    public class UserInfo
    {
        private Client _targetClient;

        public UserInfo(Client targetClient)
        {
            this._targetClient = targetClient;
        }
    }
}