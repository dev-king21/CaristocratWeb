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
namespace Caristocrate.Main.Views.Shared.Components.CarsforReview
{
    public class CarsforReviewViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public CarsforReviewViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(int categoryID, string brand_ids, string model_ids,
            string car_type = "", string rating = "",
            string min_price = "", string max_price = "",
            int sortID = 2, int limit = 10, int offset = 0)
        {
            GetCars.Response obj = new GetCars.Response();
            try
            {
                obj = getReviewedCars(categoryID, brand_ids, model_ids, car_type, rating,min_price, max_price, sortID, limit, offset);
            }
            catch (Exception ex)
            { }
            return View(obj);
        }

        public GetCars.Response getReviewedCars(int categoryID, string brand_ids, string model_ids, 
            string car_type = "", string rating = "", 
            string min_price = "", string max_price = "", 
            int sortID = 2, int limit = 10, int offset = 0)
        {

            int sortbyValue = -1;
            string sortbyFilter = "sort_by_rating";
            if (sortID == 1)
            {
                sortbyValue = 1;
                sortbyFilter = "is_for_review";
            }
            else if (sortID == 2)
            {
                sortbyValue = -1;
                sortbyFilter = "sort_by_rating";
            }
            else if (sortID == 3)
            {
                sortbyValue = 1;
                sortbyFilter = "sort_by_rating";
            }
            else if (sortID == 4)
            {
                sortbyValue = -1;
                sortbyFilter = "sort_by_review_count";
            }

            string Request = "?category_id=" + categoryID.ToString()
                            + "&brand_ids=" + brand_ids.ToString()
                            + "&model_ids=" + model_ids.ToString()
                            + "&car_type=" + car_type.ToString()
                            + "&rating=" + rating.ToString()
                            + "&min_price=" + min_price.ToString()
                            + "&max_price=" + max_price.ToString()
                            + "&"+ sortbyFilter.ToString() + "=" + sortbyValue.ToString()
                            + "&limit=" + limit.ToString() + "&offset=" + offset.ToString();

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
