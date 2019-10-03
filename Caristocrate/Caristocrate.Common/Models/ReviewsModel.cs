using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class ReviewsModel
    {
        public class Rating
        {
            public string Id { get; set; }
        }

        public class Request
        {
            public int car_id { get; set; }
            public string rating { get; set; }
            public string review_message { get; set; }
        }

        public class UserDetails
        {
            public int user_id { get; set; }
            public string user_name { get; set; }
            public string image_url { get; set; }
        }

        public class Detail
        {
            public decimal rating { get; set; }
            public int aspect_id { get; set; }
            public string aspect_title { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public decimal average_rating { get; set; }
            public string review_message { get; set; }
            public UserDetails user_details { get; set; }
            public List<Detail> details { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }

        public class ResponsebyID
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
