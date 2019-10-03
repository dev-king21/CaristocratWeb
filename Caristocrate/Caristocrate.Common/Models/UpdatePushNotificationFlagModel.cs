using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class UpdatePushNotificationFlagModel
    {
        public class Data
        {
            public int push_notification { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
