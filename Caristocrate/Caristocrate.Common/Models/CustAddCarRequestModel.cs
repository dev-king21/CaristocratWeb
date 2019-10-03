using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caristocrate.Common.Models
{
    public class CustAddCarRequestModel
    {
        public class CarAttribute
        {
            public string KEY { get; set; }
        }

        public class Request
        {
            public string name { get; set; }
            public string email { get; set; }
            public string country_code { get; set; }
            public int phone { get; set; }
            public string chassis { get; set; }
            public int kilometer { get; set; }
            public int model_id { get; set; }
            public string year { get; set; }
            public int regional_specification_id { get; set; }
            public int type_id { get; set; }
            public int engine_type_id { get; set; }
            public int transmission_type { get; set; }
            public List<CarAttribute> car_attributes { get; set; }
            public List<int> car_features { get; set; }
            //public Media media { get; set; }
            public string notes { get; set; }
            public string deleted_images { get; set; }
        }

        //public class Media {
        //    public FormFile front { get; set; }
        //    public FormFile back { get; set; }
        //    public FormFile right { get; set; }
        //    public FormFile left { get; set; }
        //    public FormFile interior { get; set; }
        //    public FormFile registration_card { get; set; }
        //}
    }
}
