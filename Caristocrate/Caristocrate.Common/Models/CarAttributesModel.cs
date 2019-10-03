using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CarAttributesModel
    {        
        public class OptionArray
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string type { get; set; }
            public List<OptionArray> option_array { get; set; }
            public string image { get; set; }
            public string name { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }
    }
}
