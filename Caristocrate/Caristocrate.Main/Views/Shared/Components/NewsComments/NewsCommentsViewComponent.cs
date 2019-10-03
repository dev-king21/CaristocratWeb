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

namespace Caristocrate.Main.Views.Shared.Components.NewsComments
{
    public class NewsCommentsViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public NewsCommentsViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(int newsID)
        {

            CommentsModel.Response obj = new CommentsModel.Response();
            try
            {
                obj = getComments(newsID);
            }
            catch (Exception ex)
            { }
            return View(obj);
        }

        public CommentsModel.Response getComments(int newsID)
        {
            string Request = "?news_id=" + newsID.ToString();

            CommentsModel.Response obj = new CommentsModel.Response();

            try
            {
                string CommentsAPIURL = AppSetting.BaseURL + AppSetting.CommentsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CommentsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
