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


namespace Caristocrate.Main.Views.Shared.Components.ChapterDetailView
{
    public class ChapterDetailViewViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }
        public int UserID { get; set; }

        public ChapterDetailViewViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
        {
            AppSetting = settings.Value;

            try
            {
                if (httpcontext.HttpContext.Session.Keys.Count() > 0)
                {
                    if (httpcontext.HttpContext.Session.GetString("UserToken").ToString() != "")
                    {
                        AuthAccessToken = httpcontext.HttpContext.Session.GetString("UserToken").ToString();
                        UserID = Convert.ToInt32(httpcontext.HttpContext.Session.GetString("UserID").ToString());
                    }
                    else
                    {
                        AuthAccessToken = AppSetting.AccessToken;
                        UserID = 0;
                    }
                }
                else
                {
                    AuthAccessToken = AppSetting.AccessToken;
                    UserID = 0;
                }
            }
            catch (Exception ex)
            {
                AuthAccessToken = AppSetting.AccessToken;
                UserID = 0;
            }
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int course_id)
        {
            ChaptersModel.ResponsebyID obj = new ChaptersModel.ResponsebyID();
            ChaptersModel.Response chapters = new ChaptersModel.Response();
            CoursesModel.ResponsebyID course = new CoursesModel.ResponsebyID();
            UserChapterAssociation.Response userchapterassociation = new UserChapterAssociation.Response();

            int chaptercount = 0;
            int userchaptercount = 0;
            decimal totalprogress = 0;

            try
            {
                obj = getChapterbyID(id);
                chapters = getChapters(course_id);
                course = getCoursebyID(course_id);
                userchapterassociation = getUserChapterAssociation();

                chaptercount = chapters.data.Count;
                userchaptercount = userchapterassociation.data.Where(a => a.chapter_id != 0 && a.user_id == UserID && a.course_id == course_id).ToList().Count;

                totalprogress = (Convert.ToDecimal(userchaptercount) / Convert.ToDecimal(chaptercount)) * 100;
                totalprogress = Math.Round(totalprogress);
            }
            catch (Exception ex)
            { }

            ViewData["chapters"] = chapters;
            ViewData["userchapterassociation"] = userchapterassociation;
            ViewData["course"] = course;
            ViewData["totalprogress"] = totalprogress.ToString();
            return View(obj);
        }

        public UserChapterAssociation.Response getUserChapterAssociation()
        {
            UserChapterAssociation.Response obj = new UserChapterAssociation.Response();
            try
            {
                string UserChapterAssociationsAPIURL = AppSetting.BaseURL + AppSetting.UserChapterAssociationsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(UserChapterAssociationsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<UserChapterAssociation.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public ChaptersModel.ResponsebyID getChapterbyID(int id)
        {
            string qs = "/" + id.ToString();
            ChaptersModel.ResponsebyID obj = new ChaptersModel.ResponsebyID();
            try
            {
                string ChaptersAPIURL = AppSetting.BaseURL + AppSetting.ChaptersAPI + qs;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ChaptersAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ChaptersModel.ResponsebyID>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public ChaptersModel.Response getChapters(int course_id)
        {
            string qs = "?course_id=" + course_id.ToString();
            ChaptersModel.Response obj = new ChaptersModel.Response();
            try
            {
                string ChaptersAPIURL = AppSetting.BaseURL + AppSetting.ChaptersAPI + qs;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ChaptersAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ChaptersModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CoursesModel.ResponsebyID getCoursebyID(int id)
        {
            CoursesModel.ResponsebyID obj = new CoursesModel.ResponsebyID();
            string qs = "/" + id.ToString();
            try
            {
                string CoursesAPIURL = AppSetting.BaseURL + AppSetting.CoursesAPI + qs;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CoursesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CoursesModel.ResponsebyID>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

    }
}
