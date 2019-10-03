using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class LoginModel
    {
        public class Request
        {
            public string email { get; set; }
            public string password { get; set; }
            public string device_type { get; set; }
            public string device_token { get; set; }
        }
    }
}
