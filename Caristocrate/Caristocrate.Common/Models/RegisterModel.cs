using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class RegisterModel
    {
        public class Details
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public object last_name { get; set; }
            public int is_verified { get; set; }
            public object dealer_type { get; set; }
            public object dealer_type_text { get; set; }
            public object country_code { get; set; }
            public object phone { get; set; }
            public object address { get; set; }
            public object image { get; set; }
            public bool email_updates { get; set; }
            public bool social_login { get; set; }
            public int region_reminder { get; set; }
            public object about { get; set; }
            public int gender { get; set; }
            public object nationality { get; set; }
            public object profession { get; set; }
            public object dob { get; set; }
            public string image_url { get; set; }
            public string gender_label { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string created_at { get; set; }
            public int push_notification { get; set; }
            public int cars_count { get; set; }
            public int favorite_count { get; set; }
            public int like_count { get; set; }
            public int view_count { get; set; }
            public Details details { get; set; }
            public object showroom_details { get; set; }
        }

        public class Data
        {
            public User user { get; set; }
            public string token { get; set; }
            public Errors errors { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }

        public class Request
        {
            public string name { get; set; }
            public string address { get; set; }
            public string country_code { get; set; }
            public string phone { get; set; }
            public string image { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string password_confirmation { get; set; }
            public string dob { get; set; }
            public string profession { get; set; }
            public string nationality { get; set; }
            public int gender { get; set; }
            public string device_token { get; set; }
            public string device_type { get; set; }
            public int region_id { get; set; }
        }

        public class Errors
        {
            public List<string> token { get; set; }
        }

        public class ForgotPasswordResponse
        {
            public bool success { get; set; }
            public List<object> data { get; set; }
            public string message { get; set; }
            public int total_count { get; set; }
        }        
    }
}
