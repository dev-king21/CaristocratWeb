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

namespace Caristocrate.Main.Views.Shared.Components.CarDetailUserReviews
{
    public class CarDetailUserReviewsViewComponent : ViewComponent
    {
        IDataProtector _dataProtector;
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public CarDetailUserReviewsViewComponent(IDataProtectionProvider provider, IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(int carID, string brand, string model, string slug, decimal average_rating)
        {
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();

            try
            {                
                Reviewsobj = getReviewsbyCarID(carID);
            }
            catch (Exception ex)
            { }

            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["brand"] = brand;
            ViewData["model"] = model;
            ViewData["slug"] = slug;
            ViewData["average_rating"] = average_rating;

            return View();
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
    }
}
