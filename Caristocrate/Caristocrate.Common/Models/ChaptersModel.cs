using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class ChaptersModel
    {
        public class Data
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
