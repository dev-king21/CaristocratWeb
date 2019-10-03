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

namespace Caristocrate.Main.Views.Shared.Components.ModelSearch
{
    public class ModelSearchViewComponent : ViewComponent
    {
        IDataProtector _dataProtector;
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public ModelSearchViewComponent(IDataProtectionProvider provider, IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(string ModelName = "", string makeID = "",
            int minPrice = 0, int maxPrice = 0, int minYear = 0, int maxYear = 0)
        {
            GetCars.Response obj = new GetCars.Response();
            try
            {
                obj = getCars(ModelName, makeID , minPrice, maxPrice, minYear, maxYear);
            }
            catch (Exception ex)
            { }
            return View(obj);
        }

        public GetCars.Response getCars(string ModelName = "", string makeID = "",
            int minPrice = 0, int maxPrice = 0, int minYear = 0, int maxYear = 0)
        {

            makeID = makeID == null ? "" : makeID;

            string Request = "?brand_ids=" + makeID.ToString()              
                + "&max_price=" + maxPrice.ToString()
                + "&min_price=" + minPrice.ToString()
                + "&max_year=" + maxYear.ToString()
                + "&min_year=" + minYear.ToString()
                + "&model_name=" + ModelName.ToString();

            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
