using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class MarkFavouriteModel
    {
        public class CarRequest
        {
            public int car_id { get; set; }            
            public int type { get; set; }
        }

        public class NewsRequest
        {
            public int news_id { get; set; }
            public int type { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public bool data { get; set; }
            public string message { get; set; }
        }
    }
}
