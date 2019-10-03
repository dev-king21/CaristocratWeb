using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class AdvertisementBannersModel
    {
        public class Request
        {
            public int banner_id { get; set; }
            public int type { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public int banner_type_id { get; set; }
            public string source_url { get; set; }
            public string video_url { get; set; }
            public string media_type { get; set; }
            public int status { get; set; }
            public string created_at { get; set; }
            public List<BannerImage> banner_images { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int is_read_more { get; set; }
        }

        public class BannerImage
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }


        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }


        public class InteractionResponse
        {
            public bool success { get; set; }
            public bool data { get; set; }
            public string message { get; set; }
        }
    }
}
