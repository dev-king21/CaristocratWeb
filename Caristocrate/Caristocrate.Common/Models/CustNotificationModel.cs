using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CustNotificationModel
    {
        public class Data
        {
            public int id { get; set; }
            public int action_type { get; set; }
            public int ref_id { get; set; }
            public string message { get; set; }
            public string created_at { get; set; }
            public int delivery_status { get; set; }
            public string image_url { get; set; }
            public string car_name { get; set; }
            public int model_year { get; set; }
            public string chassis { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
