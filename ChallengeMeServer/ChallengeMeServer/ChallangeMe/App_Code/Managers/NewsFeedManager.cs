﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChallengeMeServer.ChallengeMe.App_Code.DataAccess;
using ChallengeMeServer.Clients;
using ChallengeMeServer.Models;

namespace ChallengeMeServer.ChallangeMe.App_Code.Managers
{
    public class NewsFeedManager
    {
        public static NewsFeedManager Current { get; } = new NewsFeedManager();

        
    }
}