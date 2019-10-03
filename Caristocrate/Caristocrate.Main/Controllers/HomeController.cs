using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Caristocrate_Main.Models;
using Microsoft.AspNetCore.Http;
using Caristocrate.Common.Models;
using Microsoft.Extensions.Options;
using Caristocrate.Common;
using Caristocrate.Main.Controllers;
using System.ComponentModel;

namespace Caristocrate_Main.Controllers
{
    public class HomeController : BaseController
    {
        IHttpContextAccessor thiscontext = null;
        public HomeController(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext) : base(settings, httpcontext)
        {
            thiscontext = httpcontext;
        }

        public IActionResult Index()
        {
            GetCars.Response GetCarsobj = new GetCars.Response();
            //NewsModel.Response GetNewsobj = new NewsModel.Response();
            AdvertisementBannersModel.Response AdvertisementBannersobj = new AdvertisementBannersModel.Response();

            try
            {
                GetCarsobj = getSimilarListingCars();
                //GetNewsobj = GetNews();
                AdvertisementBannersobj = getAdvertisementBanners();
            }
            catch (Exception ex)
            { }
            //ViewData["GetNewsobj"] = GetNewsobj;
            ViewData["AdvertisementBannersobj"] = AdvertisementBannersobj;
            return View(GetCarsobj);
        }

        [Route("about")]
        public IActionResult About()
        {
            PagesModel.Response obj = new PagesModel.Response();
            try
            {
                obj = getPages();
            }
            catch (Exception ex)
            {
                
            }
            return View(obj);
        }

        [Route("terms-and-conditions")]
        public IActionResult TermandConditions()
        {
            PagesModel.Response obj = new PagesModel.Response();
            try
            {
                obj = getPages();
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        [Route("cookie-policy")]
        public IActionResult CookiePolicy()
        {
            PagesModel.Response obj = new PagesModel.Response();
            try
            {
                obj = getPages();
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        [Route("privacy-policy")]
        public IActionResult PrivacyPolicy()
        {
            PagesModel.Response obj = new PagesModel.Response();
            try
            {
                obj = getPages();
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Search(IFormCollection form)
        {
            GetCars.Request objrequest = new GetCars.Request();
            string encryptedQS = "";

            try
            {
                objrequest.categoryID = Convert.ToInt32(form["txtCategoryID"].ToString());
                objrequest.makeID = form["ddlMake"].ToString();
                objrequest.modelID = form["ddlModel"].ToString();
                objrequest.minPrice = Convert.ToInt32(form["txtMinBudget"].ToString());
                objrequest.maxPrice = Convert.ToInt32(form["txtMaxBudget"].ToString());
                objrequest.Region = form["ddlRegions"].ToString();

                string querystring = "categoryID=" + objrequest.categoryID + "&makeID=" + objrequest.makeID +
                    "&modelID=" + objrequest.modelID + "&minPrice=" + objrequest.minPrice + "&maxPrice=" + objrequest.maxPrice + "&Region=" + objrequest.Region;

                encryptedQS = General.EncryptString(querystring, AppSetting.EncryptionKey);
            }
            catch (Exception ex)
            { }

            return RedirectToAction("Index", "Car", new
            {
                data = encryptedQS,
                isfromHome = "1"
            });
        }

        public string UserSubscription(SubscriberModel.Request request)
        {
            string message = "";
            try
            {
                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                SubscriberModel.Response obj = new SubscriberModel.Response();
                string SubscribersAPIURL = AppSetting.BaseURL + AppSetting.SubscribersAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(SubscribersAPIURL, "POST", jsondata, "", 0);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<SubscriberModel.Response>(APIResponse);
                if (obj.success == true)
                {
                    message = "Success";
                }
                else
                {
                    message = obj.message;
                }
            }
            catch (Exception ex)
            {
            }

            return message;
        }

        //public IActionResult MainSearch(string searchword)
        //{
        //    GetCars.Response obj = new GetCars.Response();
        //    try
        //    {
        //        obj = getCarsforMainSearch(searchword);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    ViewData["searchword"] = searchword;
        //    return View(obj);
        //}

        public IActionResult MainSearch(string searchword)
        {
            GetCars.Request obj = new GetCars.Request();
            obj.categoryID = 28;
            obj.searchWord = searchword;
            string UserRegionIDs = "";

            try
            {
                UserRegionIDs = GetCookie("UserRegionIDs");
                if (UserRegionIDs != null)
                {
                    if (UserRegionIDs != "")
                        SetCookie("UserRegionIDs", UserRegionIDs, 365);
                }
                else
                {
                    UserRegionIDs = "";
                }
            }
            catch (Exception ex)
            { }

            obj.Region = UserRegionIDs;
            ViewData["searchword"] = searchword;
            ViewData["UserRegionIDs"] = UserRegionIDs;

            return View(obj);
        }

        public string CreateRegionCookie(string regionIDs)
        {
            string message = "";
            if (regionIDs == null)
            { regionIDs = ""; }

            try
            {
                RemoveCookie("UserRegionIDs");
                SetCookie("UserRegionIDs", regionIDs, 365);
            }
            catch (Exception ex)
            { }

            return message;
        }

        public int GetCustomerNotificationCount()
        {
            int count = 0;
            CustNotificationModel.Response obj = new CustNotificationModel.Response();
            try
            {
                if (UserEmail != "")
                {
                    obj = GetCustomerNotifications();
                    if (obj.success == true)
                    {
                        count = obj.data.Where(a => a.delivery_status == 20).ToList().Count;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return count;
        }


        #region ViewComponent Calls

        public IActionResult HomeSearch()
        {
            return ViewComponent("HomeSearch");
        }

        public IActionResult HeaderMenu()
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            return ViewComponent("HeaderMenu" , new { categories  = categories });
        }

        public IActionResult FooterMenu()
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            return ViewComponent("FooterMenu", new { categories = categories });
        }

        public IActionResult HomePageNews()
        {            
            return ViewComponent("HomePageNews");
        }

        public IActionResult HeaderRegions()
        {
            string UserRegionIDs = "";
            try
            {
                UserRegionIDs = GetCookie("UserRegionIDs");
                if (UserRegionIDs != null)
                {
                    if (UserRegionIDs != "")
                        SetCookie("UserRegionIDs", UserRegionIDs, 365);
                }
                else
                {
                    UserRegionIDs = "";
                }
            }
            catch (Exception ex)
            { }
            return ViewComponent("HeaderRegions", new { UserRegionIDs = UserRegionIDs });
        }

        [Route("car-detail/{type}/{id}")]
        public IActionResult CarDetailView(int type, int id)
        {
            string controllername = "Car";
            string methodname = "CarDetail";
            string makeslug = "";
            string modelslug = "";
            string carslug = "";
            GetCarDetail.Response obj = new GetCarDetail.Response();
            try
            {
                obj = getCarDetails(id);
                if (obj.success == true)
                {
                    carslug = obj.data.slug;
                    makeslug = obj.data.car_model.brand.slug;
                    modelslug = obj.data.car_model.slug;

                    if (obj.data.category.id != 28)
                    {
                        methodname = "PreOwnedCarDetail";
                    }
                }
            }
            catch (Exception ex)
            { }

            return RedirectToAction(methodname, controllername, new { brand = makeslug, model = modelslug, id = carslug });
        }

        public IActionResult SearchLeftFilter(GetCars.Request obj = null)
        {
            return ViewComponent("SearchLeftFilter", new { obj = obj });
        }

        public IActionResult SearchFilteredCars(GetCars.Request obj = null)
        {
            return ViewComponent("SearchFilteredCars", new { obj = obj });
        }

        #endregion




        #region API Calls

        public GetCars.Response getSimilarListingCars()
        {
            string Request = "?limit=30&offset=0&is_featured=1";

            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPIV2 + Request;
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

        public GetCars.Response getCars()
        {
            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getCarsforMainSearch(string searchkeyword)
        {
            GetCars.Response obj = new GetCars.Response();
            try
            {
                string Request = "?category_id=28"
                            + "&car_title=" + searchkeyword.ToString();                            

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

        public AdvertisementBannersModel.Response getAdvertisementBanners()
        {
            AdvertisementBannersModel.Response obj = new AdvertisementBannersModel.Response();
            try
            {
                string AdvertisementBannersAPIURL = AppSetting.BaseURL + AppSetting.AdvertisementBannersAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(AdvertisementBannersAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<AdvertisementBannersModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public NewsModel.Response GetNews()
        {
            NewsModel.Response obj = new NewsModel.Response();
            try
            {
                string NewsAPIURL = AppSetting.BaseURL + AppSetting.NewsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(NewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public PagesModel.Response getPages()
        {
            PagesModel.Response obj = new PagesModel.Response();
            try
            {
                string PagesAPIURL = AppSetting.BaseURL + AppSetting.PagesAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(PagesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<PagesModel.Response>(APIResponse);
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
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCarDetail.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CustNotificationModel.Response GetCustomerNotifications()
        {
            CustNotificationModel.Response obj = new CustNotificationModel.Response();
            try
            {
                string CustNotificationAPIURL = AppSetting.BaseURL + AppSetting.CustNotificationAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustNotificationAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustNotificationModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        #endregion

        #region Cookie Methods

        //public string GetCookie(string key)
        //{
        //    return Request.Cookies[key];
        //}

        //public void SetCookie(string key, string value, int? expireTime)
        //{
        //    CookieOptions option = new CookieOptions();
        //    if (expireTime.HasValue)
        //        option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        //    else
        //        option.Expires = DateTime.Now.AddMilliseconds(10);
        //    Response.Cookies.Append(key, value, option);
        //}

        //public void RemoveCookie(string key)
        //{
        //    Response.Cookies.Delete(key);
        //}

        #endregion
    }
}
