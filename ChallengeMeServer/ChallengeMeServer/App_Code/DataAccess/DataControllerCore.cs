﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.DataController
{
    public class DataControllerCore
    {
        static readonly DataControllerCore _dataControllerCore = new DataControllerCore();

        public static DataControllerCore DataController
        {
            get
            {
                return _dataControllerCore;
            }
        }

        private Object _lockObject = new Object();

       
    }
}