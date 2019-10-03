using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class SubscriberModel
    {
        public class Request
        {
            public string email { get; set; }
        }


        public class Data
        {
            public string email { get; set; }
            public int id { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
