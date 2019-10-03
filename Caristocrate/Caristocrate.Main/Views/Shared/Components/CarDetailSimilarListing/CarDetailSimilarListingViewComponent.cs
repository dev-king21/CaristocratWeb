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

namespace Caristocrate.Main.Views.Shared.Components.CarDetailSimilarListing
{
    public class CarDetailSimilarListingViewComponent : ViewComponent
    {
        IDataProtector _dataProtector;
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public CarDetailSimilarListingViewComponent(IDataProtectionProvider provider, IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(int categoryID, int carID, string brand, string model, string slug)
        {
            GetCars.Response GetCarsobj = new GetCars.Response();

            try
            {
                GetCarsobj = getSimilarListingCars(categoryID);
                
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["carID"] = carID;
            ViewData["brand"] = brand;
            ViewData["model"] = model;
            ViewData["slug"] = slug;

            return View();
        }

        public GetCars.Response getSimilarListingCars(int categoryID)
        {
            string Request = "?category_id=" + categoryID.ToString()
                            + "&limit=10&offset=0";

            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
