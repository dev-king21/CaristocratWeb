using Caristocrate.Common;
using Caristocrate.Common.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization.Json;

namespace Caristocrate.DAL
{
    public class DataAccess
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }
        IOptions<AppSettings> settings 

        public GetCars.Response getCars()
        {
            string GetFilteredCarsAPI = AppSetting.GetFilteredCarsAPI;
            string APIResponse = "";
            APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
            GetCars.Response obj = new GetCars.Response();
            APIResponse = objAPI.GetResponse();
            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            return obj;
        }
    }
}
