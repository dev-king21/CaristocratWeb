using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class SettingModel
    {
        public class Translations
        {
            public int id { get; set; }
            public int setting_id { get; set; }
            public string locale { get; set; }
            public object title { get; set; }
            public object address { get; set; }
            public object about { get; set; }
            public string ask_for_consultancy { get; set; }
            public string personal_shopper { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public object deleted_at { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string default_language { get; set; }
            public object email { get; set; }
            public string logo { get; set; }
            public object phone { get; set; }
            public object latitude { get; set; }
            public object longitude { get; set; }
            public int? depreciation_trend { get; set; }
            public object limit_for_cars { get; set; }
            public object limit_for_featured_cars { get; set; }
            public object playstore { get; set; }
            public object appstore { get; set; }
            public object social_links { get; set; }
            public int? app_version { get; set; }
            public string force_update { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public object deleted_at { get; set; }
            public object title { get; set; }
            public object address { get; set; }
            public object about { get; set; }
            public string personal_shopper { get; set; }
            public string ask_for_consultancy { get; set; }
            public List<Translations> translations { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
