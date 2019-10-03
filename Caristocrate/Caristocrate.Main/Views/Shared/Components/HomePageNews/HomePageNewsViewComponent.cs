using Caristocrate.Common;
using Caristocrate.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Caristocrate.Main.Views.Shared.Components.HomePageNews
{
    public class HomePageNewsViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public HomePageNewsViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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
            NewsModel.Response GetNewsobj = new NewsModel.Response();
            NewsModel.Response GetNewsobj2 = new NewsModel.Response();
            NewsModel.Response GetNewsobj3 = new NewsModel.Response();
            try
            {
                GetNewsobj = GetNews(1);
                GetNewsobj2 = GetNews(2);
                GetNewsobj3 = GetNews(3);
            }
            catch (Exception ex)
            { }
            ViewData["GetNewsobj"] = GetNewsobj;
            ViewData["GetNewsobj2"] = GetNewsobj2;
            ViewData["GetNewsobj3"] = GetNewsobj3;
            return View();
        }

        public NewsModel.Response GetNews(int category_Id)
        {
            NewsModel.Response obj = new NewsModel.Response();
            string Request = "?limit=3&offset=0&category_id=" + category_Id.ToString();
            try
            {
                string NewsAPIURL = AppSetting.BaseURL + AppSetting.NewsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(NewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
