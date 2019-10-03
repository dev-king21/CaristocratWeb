using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class RegionsModel
    {
        public class Data
        {
            public int id { get; set; }
            public string name { get; set; }
            public string currency { get; set; }
            public int is_my_region { get; set; }
            public string flag { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
