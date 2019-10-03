using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class GetMake
    {
        public class Media
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
            public string name { get; set; }
            public string slug { get; set; }
            public List<Media> media { get; set; }
            public Errors errors { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }

        public class Errors
        {
            public List<string> token { get; set; }
        }
    }
}
