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

namespace Caristocrate.Main.Views.Shared.Components.CarReviews
{
    public class CarReviewsViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public CarReviewsViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(int carID)
        {
            GetCarDetail.Response cardetails = new GetCarDetail.Response();
            ReviewsModel.Response obj = new ReviewsModel.Response();
            decimal overallcarRating = 0;

            try
            {
                obj = getReviewsbyCarID(carID);
                cardetails = getCarDetails(carID);

                if (cardetails.success == true)
                {
                    overallcarRating = cardetails.data.average_rating;
                }

            }
            catch (Exception ex)
            { }
            ViewData["overallcarRating"] = overallcarRating;
            return View(obj);
        }

        public ReviewsModel.Response getReviewsbyCarID(int car_id)
        {
            string Request = "?car_id=" + car_id.ToString();

            ReviewsModel.Response obj = new ReviewsModel.Response();

            try
            {
                string ReviewsAPIURL = AppSetting.BaseURL + AppSetting.ReviewsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ReviewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReviewsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCarDetail.Response getCarDetails(int id)
        {
            GetCarDetail.Response obj = new GetCarDetail.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + "/" + id;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCarDetail.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

    }
}
