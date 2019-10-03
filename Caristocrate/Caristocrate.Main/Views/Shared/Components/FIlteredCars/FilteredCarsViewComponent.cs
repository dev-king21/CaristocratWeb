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

namespace Caristocrate.Main.Views.Shared.Components.FIlteredCars
{
    public class FilteredCarsViewComponent : ViewComponent
    {
        IDataProtector _dataProtector;
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public FilteredCarsViewComponent(IDataProtectionProvider provider, IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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
            
            GetCars.Response getCarsobj = new GetCars.Response();
            try
            {
                getCarsobj = getCars(obj);
            }
            catch (Exception ex)
            { }
            return View(getCarsobj);
        }

        public GetCars.Response getCars(GetCars.Request requestOBJ)
        {
            requestOBJ.makeID = requestOBJ.makeID == null ? "": requestOBJ.makeID;
            requestOBJ.modelID = requestOBJ.modelID == null ? "": requestOBJ.modelID;
            requestOBJ.CarType = requestOBJ.CarType == null ? "": requestOBJ.CarType;
            requestOBJ.Region = requestOBJ.Region == null ? "" : requestOBJ.Region;

            string Request = "?category_id=" + requestOBJ.categoryID.ToString()
                + "&brand_ids=" + requestOBJ.makeID.ToString()
                + "&model_ids=" + requestOBJ.modelID.ToString()
                + "&max_price=" + requestOBJ.maxPrice.ToString()
                + "&min_price=" + requestOBJ.minPrice.ToString()
                + "&max_year=" + requestOBJ.maxYear.ToString()
                + "&min_year=" + requestOBJ.minYear.ToString()
                + "&max_mileage=" + requestOBJ.maxKM.ToString()
                + "&min_mileage=" + requestOBJ.minKM.ToString()
                + "&car_type=" + requestOBJ.CarType.ToString()
                + "&car_sub_type=" + requestOBJ.SubSegmentID.ToString()
                + "&version_id=" + requestOBJ.versionID.ToString()                
                + "&regions=" + requestOBJ.Region.ToString()
                + "&sort_by_year=" + requestOBJ.sortbyYear.ToString()
                + "&dealer=" + requestOBJ.dealer.ToString();

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
