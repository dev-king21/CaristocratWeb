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

namespace Caristocrate.Main.Views.Shared.Components.HeaderRegions
{
    public class HeaderRegionsViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public HeaderRegionsViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(string UserRegionIDs)
        {

            RegionsModel.Response regions = new RegionsModel.Response();
            try
            {
                regions = getRegions();

                ViewData["UserRegionIDs"] = UserRegionIDs;

            }
            catch (Exception ex)
            { }
            return View(regions);
        }

        public RegionsModel.Response getRegions()
        {
            RegionsModel.Response obj = new RegionsModel.Response();
            try
            {
                string RegionsAPIURL = AppSetting.BaseURL + AppSetting.RegionsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(RegionsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RegionsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
