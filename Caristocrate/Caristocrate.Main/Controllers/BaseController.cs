using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caristocrate.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Caristocrate.Common;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Caristocrate.Main.Controllers
{
    public class BaseController : Controller
    {
        public AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserEmail { get; set; }
        public string UserCountryCode { get; set; }
        public string UserPhoneNo { get; set; }
        public string UserEnrolled { get; set; }

        public BaseController(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
        {
            
            AppSetting = settings.Value;
            try
            {
                UserName = "";
                UserImage = "";
                UserEmail = "";
                UserCountryCode = "";
                UserPhoneNo = "";
                UserEnrolled = "";

                if (httpcontext.HttpContext.Session.Keys.Count() > 0)
                {
                    if (httpcontext.HttpContext.Session.GetString("UserToken").ToString() != "")
                    {
                        AuthAccessToken = httpcontext.HttpContext.Session.GetString("UserToken").ToString();
                        UserName = httpcontext.HttpContext.Session.GetString("UserName").ToString();
                        UserImage = httpcontext.HttpContext.Session.GetString("UserImage").ToString();
                        UserEmail = httpcontext.HttpContext.Session.GetString("UserEmail").ToString();
                        UserCountryCode = httpcontext.HttpContext.Session.GetString("UserCountryCode").ToString();
                        UserPhoneNo = httpcontext.HttpContext.Session.GetString("UserPhone").ToString();
                        UserEnrolled = httpcontext.HttpContext.Session.GetString("UserEnrolled").ToString();
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

                try
                {
                    if (ViewData["categories"] == null)
                    {
                        CategoryModel.Response categories = new CategoryModel.Response();
                        categories = GetBaseCategories();
                        ViewData["categories"] = categories;
                        ViewData["Maincategories"] = categories;
                    }                    

                    ViewData["DelaerLoginURL"] = AppSetting.DelaerLoginURL;

                    CreateBaseRegionCookie();
                }
                catch (Exception ex)
                {
                }

            }
            catch (Exception ex)
            {
                AuthAccessToken = AppSetting.AccessToken;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ViewData["categories"] == null)
            {
                CategoryModel.Response categories = new CategoryModel.Response();
                categories = GetBaseCategories();
                ViewData["categories"] = categories;
                ViewData["Maincategories"] = categories;
            }

            ViewData["DelaerLoginURL"] = AppSetting.DelaerLoginURL;

            CreateBaseRegionCookie();
        }

        public void CreateBaseRegionCookie()
        {
            try
            {
                RegionsModel.Response regions = new RegionsModel.Response();
                string UserRegionIDs = GetCookie("UserRegionIDs");
                if (UserRegionIDs == null)
                {
                    regions = getRegions();
                    if (regions.success == true)
                    {
                        if (regions.data.Count == 1)
                        {
                            UserRegionIDs = regions.data.FirstOrDefault().id.ToString();
                            SetCookie("UserRegionIDs", UserRegionIDs, 365);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
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

        public CategoryModel.Response GetBaseCategories()
        {
            CategoryModel.Response obj = new CategoryModel.Response();
            try
            {
                string CategoryAPIURL = AppSetting.BaseURL + AppSetting.CategoryAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CategoryAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        #region Cookie Methods

        public string GetCookie(string key)
        {
            return Request.Cookies[key];
        }

        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }

        public void RemoveCookie(string key)
        {
            Response.Cookies.Delete(key);
        }

        #endregion

    }
}