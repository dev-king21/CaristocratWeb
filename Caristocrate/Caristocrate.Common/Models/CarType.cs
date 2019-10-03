using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CarType
    {        
        public class ChildType
        {
            public int id { get; set; }
            public string slug { get; set; }
            public string image { get; set; }
            public string selected_icon { get; set; }
            public string un_selected_icon { get; set; }
            public string name { get; set; }
            public List<object> child_types { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string slug { get; set; }
            public string image { get; set; }
            public string selected_icon { get; set; }
            public string un_selected_icon { get; set; }
            public string name { get; set; }
            public List<ChildType> child_types { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<Data> data { get; set; }
            public string message { get; set; }
        }
    }
}
