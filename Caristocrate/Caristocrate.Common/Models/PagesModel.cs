using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class PagesModel
    {
        public class Translation
        {
            public int id { get; set; }
            public int page_id { get; set; }
            public string locale { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public bool status { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public object deleted_at { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string slug { get; set; }
            public bool status { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public object deleted_at { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public List<Translation> translations { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
