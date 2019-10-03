using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class RegionalSpecificationModel
    {
        public class Data
        {
            public int id { get; set; }
            public string created_at { get; set; }
            public string name { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
