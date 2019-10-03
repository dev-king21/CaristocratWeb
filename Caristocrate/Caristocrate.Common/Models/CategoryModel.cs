using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CategoryModel
    {
        public class Medium
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Medium2
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string slug { get; set; }
            public int user_id { get; set; }
            public int views_count { get; set; }
            public int type { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public int unread_count { get; set; }
            public List<BannerImage> banner_images { get; set; }
            public List<Image> images { get; set; }
            public string name { get; set; }
            public string subtitle { get; set; }
            public object description { get; set; }
            public List<Medium> media { get; set; }
            public List<ChildCategory> child_category { get; set; }
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

        public class Image
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class ChildCategory
        {
            public int id { get; set; }
            public string slug { get; set; }
            public int user_id { get; set; }
            public int views_count { get; set; }
            public int type { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public int unread_count { get; set; }
            public string name { get; set; }
            public string subtitle { get; set; }
            public object description { get; set; }
            public List<Medium2> media { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
