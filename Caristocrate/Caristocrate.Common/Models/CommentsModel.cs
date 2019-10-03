using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CommentsModel
    {
        
        public class Request
        {
            public int parent_id { get; set; }
            public int news_id { get; set; }
            public string comment_text { get; set; }
        }

        public class RegionDetail
        {
            public int id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class Details
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public object last_name { get; set; }
            public int? is_verified { get; set; }
            public object dealer_type { get; set; }
            public object dealer_type_text { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public object address { get; set; }
            public string image { get; set; }
            public bool email_updates { get; set; }
            public bool social_login { get; set; }
            public int? region_id { get; set; }
            public int region_reminder { get; set; }
            public object limit_for_cars { get; set; }
            public object limit_for_featured_cars { get; set; }
            public object expiry_date { get; set; }
            public string about { get; set; }
            public int? gender { get; set; }
            public string nationality { get; set; }
            public string profession { get; set; }
            public string dob { get; set; }
            public string image_url { get; set; }
            public string gender_label { get; set; }
            public RegionDetail region_detail { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string created_at { get; set; }
            public int? push_notification { get; set; }
            public int? cars_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? view_count { get; set; }
            public Details details { get; set; }
            public object showroom_details { get; set; }
        }

        public class RegionDetail2
        {
            public int id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class Details2
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public object last_name { get; set; }
            public int? is_verified { get; set; }
            public object dealer_type { get; set; }
            public object dealer_type_text { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public object address { get; set; }
            public string image { get; set; }
            public bool email_updates { get; set; }
            public bool social_login { get; set; }
            public int? region_id { get; set; }
            public int? region_reminder { get; set; }
            public object limit_for_cars { get; set; }
            public object limit_for_featured_cars { get; set; }
            public object expiry_date { get; set; }
            public string about { get; set; }
            public int? gender { get; set; }
            public string nationality { get; set; }
            public string profession { get; set; }
            public string dob { get; set; }
            public string image_url { get; set; }
            public string gender_label { get; set; }
            public RegionDetail2 region_detail { get; set; }
        }

        public class Userwithtrash
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string created_at { get; set; }
            public int push_notification { get; set; }
            public int? cars_count { get; set; }
            public int? favorite_count { get; set; }
            public int? like_count { get; set; }
            public int? view_count { get; set; }
            public Details2 details { get; set; }
            public object showroom_details { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string comment_text { get; set; }
            public int parent_id { get; set; }
            public int news_id { get; set; }
            public int user_id { get; set; }
            public int status { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public User user { get; set; }
            public Userwithtrash userwithtrash { get; set; }
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
