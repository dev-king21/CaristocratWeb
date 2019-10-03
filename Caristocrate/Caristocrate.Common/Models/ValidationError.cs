using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class ValidationError
    {
        public class Errors
        {
            public List<string> token { get; set; }
        }

        public class Data
        {
            public Errors errors { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
        }
    }
}
