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

namespace Caristocrate.Main.Views.Shared.Components.SearchLeftFilter
{
    public class SearchLeftFilterViewComponent : ViewComponent
    {
        IDataProtector _dataProtector;
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public SearchLeftFilterViewComponent(IDataProtectionProvider provider, IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
        {
            _dataProtector = provider.CreateProtector("QueryStringProtection");
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

        public async Task<IViewComponentResult> InvokeAsync(GetCars.Request obj)
        {
            try
            {
                GetMake.Response getMakeobj = new GetMake.Response();
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;

                GetModel.Response getModelobj = new GetModel.Response();
                getModelobj = getModel();
                ViewData["GetModel"] = getModelobj;

                CarType.Response CarTypeobj = new CarType.Response();
                CarTypeobj = GetcarTypes();
                ViewData["CarTypeobj"] = CarTypeobj;

                RegionsModel.Response getRegionsobj = new RegionsModel.Response();
                getRegionsobj = getRegions();
                ViewData["getRegionsobj"] = getRegionsobj;

                VersionsModel.Response getVersionsobj = new VersionsModel.Response();
                getVersionsobj = getVersions();
                ViewData["getVersionsobj"] = getVersionsobj;
            }
            catch (Exception ex)
            { }
            return View(obj);
        }

        public VersionsModel.Response getVersions()
        {
            VersionsModel.Response obj = new VersionsModel.Response();
            try
            {
                string CarVersionsAPIURL = AppSetting.BaseURL + AppSetting.CarVersionsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CarVersionsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<VersionsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
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

        public CarType.Response GetcarTypes()
        {
            CarType.Response obj = new CarType.Response();
            try
            {
                string carTypesAPIURL = AppSetting.BaseURL + AppSetting.carTypesAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(carTypesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CarType.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
