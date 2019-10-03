using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class BankRateModel
    {
        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }

        public class Medium
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
            public string title { get; set; }
            public string phone_no { get; set; }
            public string address { get; set; }
            public double rate { get; set; }
            public int type { get; set; }
            public string created_at { get; set; }
            public List<Medium> media { get; set; }
        }
    }
}
