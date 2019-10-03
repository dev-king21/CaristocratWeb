using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CoursesModel
    {
        public class Category
        {
            public int id { get; set; }
            public string slug { get; set; }
            public int user_id { get; set; }
            public int views_count { get; set; }
            public int type { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public int unread_count { get; set; }
            public List<object> banner_images { get; set; }
            public List<object> images { get; set; }
            public string name { get; set; }
            public string subtitle { get; set; }
            public object description { get; set; }
            public List<Media2> media { get; set; }
            public List<object> child_category { get; set; }
        }

        public class Media
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Media2
        {
            public int id { get; set; }
            public int media_type { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public string created_at { get; set; }
            public string file_url { get; set; }
        }

        public class Chapter
        {
            public int id { get; set; }
            public int course_id { get; set; }
            public string link { get; set; }
            public string duration { get; set; }
            public string created_at { get; set; }
            public string title { get; set; }
            public string subtitle { get; set; }
            public object description { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string location { get; set; }
            public string language { get; set; }
            public string duration { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public int price { get; set; }
            public string currency { get; set; }
            public string region { get; set; }
            public object intro_link { get; set; }
            public string created_at { get; set; }
            public string slug { get; set; }
            public string title { get; set; }
            public string subtitle { get; set; }
            public string description { get; set; }
            public string about { get; set; }
            public List<Media> media { get; set; }
            public Category category { get; set; }
            public List<Chapter> chapters { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
            public int total_count { get; set; }
        }

        public class ResponsebyID
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
            public int total_count { get; set; }
        }
    }
}
