using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class NewsModel
    {
        public class Medium
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
            public string thumnail { get; set; }
        }

        public class Meta
        {
            public int id { get; set; }
            public string title { get; set; }
            public string tags { get; set; }
            public string description { get; set; }
        }


        public class Data
        {
            public int id { get; set; }
            public int category_id { get; set; }
            public object user_id { get; set; }
            public int views_count { get; set; }
            public int favorite_count { get; set; }
            public int like_count { get; set; }
            public int comments_count { get; set; }
            public int is_featured { get; set; }
            public string slug { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string link { get; set; }
            public bool is_liked { get; set; }
            public bool is_viewed { get; set; }
            public bool is_favorite { get; set; }
            public string source_image_url { get; set; }
            public string headline { get; set; }
            public string description { get; set; }
            public string source { get; set; }
            public string related_car { get; set; }
            public List<Medium> media { get; set; }
            public List<Meta> meta { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
