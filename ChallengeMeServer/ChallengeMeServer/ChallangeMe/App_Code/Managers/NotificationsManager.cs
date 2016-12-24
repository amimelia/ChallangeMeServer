using ChallengeMeServer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.ChallangeMe.App_Code.Managers
{
    public class NotificationsManager
    {
        public static NotificationsManager Current { get; } = new NotificationsManager();
    }
}