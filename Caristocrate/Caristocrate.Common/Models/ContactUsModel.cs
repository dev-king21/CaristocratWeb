using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class ContactUsModel
    {
        public class RequestOnsiteCourseEnquiry
        {
            public string message { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string country_code { get; set; }
            public int type { get; set; }
        }

        public class RequestPersonalCoachForm
        {
            public string message { get; set; }
            public string name { get; set; }
            public string industry { get; set; }
            public string company { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int type { get; set; }
        }

        public class RequestWithoutCarID
        {
            public int bank_id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public int type { get; set; }
        }

        public class Request
        {
            public int car_id { get; set; }
            public int bank_id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public int type { get; set; }
            public string message { get; set; }
        }

        public class Data
        {
            public int car_id { get; set; }
            public int bank_id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public int type { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
