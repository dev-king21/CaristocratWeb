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

namespace Caristocrate.Main.Views.Shared.Components.HomeSearch
{    
    public class HomeSearchViewComponent : ViewComponent
    {        
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public HomeSearchViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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
            try
            {
                GetMake.Response getMakeobj = new GetMake.Response();
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;

                GetModel.Response getModelobj = new GetModel.Response();
                getModelobj = getModel();
                ViewData["GetModel"] = getModelobj;

                RegionsModel.Response getRegionsobj = new RegionsModel.Response();
                getRegionsobj = getRegions();
                ViewData["getRegionsobj"] = getRegionsobj;
            }
            catch (Exception ex)
            { }
            return View();
            //return Task.FromResult<IViewComponentResult>(View("Default"));
            //return await Task.FromResult((IViewComponentResult)View("Default"));
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

        public GetModel.Response getModel()
        {
            GetModel.Response obj = new GetModel.Response();
            try
            {
                string GetMakeAPIURL = AppSetting.BaseURL + AppSetting.GetModelsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetMakeAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
