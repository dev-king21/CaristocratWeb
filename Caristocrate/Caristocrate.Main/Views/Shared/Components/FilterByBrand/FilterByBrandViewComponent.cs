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
using Microsoft.AspNetCore.Session;
using System.Runtime.Serialization.Json;

namespace Caristocrate.Main.Views.Shared.Components.FilterByBrand
{
    public class FilterByBrandViewComponent : ViewComponent
    {        
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public FilterByBrandViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
        {            
            AppSetting = settings.Value;
            try
            {
                if (httpcontext.HttpContext.Session.Keys.Count() > 0)
                {
                    if (httpcontext.HttpContext.Session.GetString("UserToken").ToString() != "")
                    {
                        AuthAccessToken = httpcontext.HttpContext.Session.GetString("UserToken").ToString();
                    }
                    else
                    {
                        AuthAccessToken = AppSetting.AccessToken;
                    }
                }
                else
                {
                    AuthAccessToken = AppSetting.AccessToken;
                }
            }
            catch (Exception ex)
            {
                AuthAccessToken = AppSetting.AccessToken;
            }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GetMake.Response getMakeobj = new GetMake.Response();
            try
            {                
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;                
            }
            catch (Exception ex)
            { }
            return View(getMakeobj);
        }

        public GetMake.Response getMake()
        {
            GetMake.Response obj = new GetMake.Response();
            try
            {
                string GetMakeAPIURL = AppSetting.BaseURL + AppSetting.GetBrandsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetMakeAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMake.Response>(APIResponse);
                obj.data = obj.data.OrderBy(a => a.name).ToList();
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
    
}
