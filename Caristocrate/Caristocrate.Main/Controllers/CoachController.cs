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

namespace Caristocrate.Main.Controllers
{
    public class CoachController : Controller
    {

        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }
        public bool IsLogin { get; set; }
        public int UserNotificationFlag { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserEmail { get; set; }
        public string UserCountryCode { get; set; }
        public string UserPhoneNo { get; set; }
        public string UserEnrolled { get; set; }
        public int UserID { get; set; }
        private IHostingEnvironment _hostingEnvironment;

        public CoachController(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext, IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            AppSetting = settings.Value;

            try
            {
                UserID = 0;
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
                        UserID = Convert.ToInt32(httpcontext.HttpContext.Session.GetString("UserID").ToString());
                        UserName = httpcontext.HttpContext.Session.GetString("UserName").ToString();
                        UserImage = httpcontext.HttpContext.Session.GetString("UserImage").ToString();
                        UserEmail = httpcontext.HttpContext.Session.GetString("UserEmail").ToString();
                        UserCountryCode = httpcontext.HttpContext.Session.GetString("UserCountryCode").ToString();
                        UserPhoneNo = httpcontext.HttpContext.Session.GetString("UserPhone").ToString();
                        UserNotificationFlag = Convert.ToInt32(httpcontext.HttpContext.Session.GetString("UserNotificationFlag").ToString());
                        UserEnrolled = httpcontext.HttpContext.Session.GetString("UserEnrolled").ToString();
                        IsLogin = true;
                    }
                    else
                    {
                        AuthAccessToken = AppSetting.AccessToken;
                        IsLogin = false;
                    }
                }
                else
                {
                    AuthAccessToken = AppSetting.AccessToken;
                    IsLogin = false;
                }
            }
            catch (Exception ex)
            {
                AuthAccessToken = AppSetting.AccessToken;
                IsLogin = false;
            }
        }
        [Route("luxury-coach")]
        public IActionResult Index()
        {
            CoursesModel.Response onlineCoursesobj = new CoursesModel.Response();
            CoursesModel.Response onsiteCoursesobj = new CoursesModel.Response();
            PagesModel.Response obj = new PagesModel.Response();         

            int OnsiteCoursesID = Convert.ToInt32(AppSetting.OnsiteCoursesID);
            int OnlineCoursesID = Convert.ToInt32(AppSetting.OnlineCoursesID);
            try
            {
                onsiteCoursesobj = getCourses(OnsiteCoursesID);
                onlineCoursesobj = getCourses(OnlineCoursesID);
                obj = getPages();
            }
            catch (Exception ex)
            {
            }

            ViewData["onsiteCoursesobj"] = onsiteCoursesobj;
            ViewData["onlineCoursesobj"] = onlineCoursesobj;

            return View(obj);
        }
        [Route("~/online-course/{id}")]
        public IActionResult OnlineCourses(string id)
        {
            CoursesModel.ResponsebyID obj = new CoursesModel.ResponsebyID();
            UserChapterAssociation.Response userchapterassociation = new UserChapterAssociation.Response();
            string IsEnrolled = "0";
            CoursesModel.Response onlineCoursesobj = new CoursesModel.Response();                                    
            int OnlineCoursesID = Convert.ToInt32(AppSetting.OnlineCoursesID);

            try
            {
                onlineCoursesobj = getCourses(OnlineCoursesID);
                int ObjectID = onlineCoursesobj.data.Where(a => a.slug == id).FirstOrDefault().id;

                if (UserName != "")
                {
                    userchapterassociation = getUserChapterAssociation();
                    if (userchapterassociation.data.Where(a => a.course_id == ObjectID && a.chapter_id == 0 && a.user_id == UserID).ToList().Count > 0)
                    {
                        IsEnrolled = "1";
                    }
                }
                
                obj = getCoursebyID(ObjectID);
            }
            catch (Exception ex)
            {                
            }

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;
            ViewData["UserEnrolled"] = UserEnrolled;

            ViewData["IsEnrolled"] = IsEnrolled;

            return View(obj);
        }
        [Route("~/onsite-course/{id}")]
        public IActionResult OnsiteCourses(string id)
        {
            CoursesModel.ResponsebyID obj = new CoursesModel.ResponsebyID();
            UserChapterAssociation.Response userchapterassociation = new UserChapterAssociation.Response();
            string IsEnrolled = "0";
            CoursesModel.Response onsiteCoursesobj = new CoursesModel.Response();
            int OnsiteCoursesID = Convert.ToInt32(AppSetting.OnsiteCoursesID);

            try
            {
                onsiteCoursesobj = getCourses(OnsiteCoursesID);
                int ObjectID = onsiteCoursesobj.data.Where(a => a.slug == id).FirstOrDefault().id;

                if (UserName != "")
                {
                    userchapterassociation = getUserChapterAssociation();
                    if (userchapterassociation.data.Where(a => a.course_id == ObjectID && a.chapter_id == 0 && a.user_id == UserID).ToList().Count > 0)
                    {
                        IsEnrolled = "1";
                    }
                }
                obj = getCoursebyID(ObjectID);                
            }
            catch (Exception ex)
            {
            }

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;
            ViewData["UserEnrolled"] = UserEnrolled;

            ViewData["IsEnrolled"] = IsEnrolled;
            return View(obj);
        }
        [Route("~/luxury-coach/one-on-one-coaching")]
        public IActionResult PersonalCoach()
        {
            PagesModel.Response obj = new PagesModel.Response();
            try
            {
                obj = getPages();
            }
            catch (Exception ex)
            {

            }

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View(obj);
        }

        public IActionResult ChapterDetail(int id, int course_id)
        {
            if (IsLogin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            ChaptersModel.ResponsebyID obj = new ChaptersModel.ResponsebyID();
            ChaptersModel.Response chapters = new ChaptersModel.Response();
            UserChapterAssociation.Response userchapterassociation = new UserChapterAssociation.Response();
            CoursesModel.ResponsebyID course = new CoursesModel.ResponsebyID();
            int chaptercount = 0;
            int userchaptercount = 0;
            decimal totalprogress = 0;
            string message = "";

            try
            {
                UserChapterAssociation.Request request = new UserChapterAssociation.Request();
                request.course_id = course_id;
                request.chapter_id = id;
                message = MarkUserChapterAssociation(request);

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

            ViewData["userchapterassociation"] = userchapterassociation;
            ViewData["chapters"] = chapters;
            ViewData["course"] = course;
            ViewData["totalprogress"] = totalprogress.ToString();
            return View(obj);            
        }


        public string PersonalCoachForm(ContactUsModel.RequestPersonalCoachForm obj)
        {
            string message = "Failed";
            try
            {
                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                ContactUsModel.Response response = new ContactUsModel.Response();

                string ContactusAPI = AppSetting.BaseURL + AppSetting.ContactusAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ContactusAPI, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactUsModel.Response>(APIResponse);

                if (response.success == true)
                {
                    message = response.message;
                }
                else
                {
                    message = "Something went Wrong";
                }
            }
            catch (Exception ex)
            { message = "Something went Wrong"; }
            return message;
        }

        public string OnsiteCourseEnquiry(ContactUsModel.RequestOnsiteCourseEnquiry obj)
        {
            string message = "Failed";
            try
            {
                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                ContactUsModel.Response response = new ContactUsModel.Response();

                string ContactusAPI = AppSetting.BaseURL + AppSetting.ContactusAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ContactusAPI, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactUsModel.Response>(APIResponse);

                if (response.success == true)
                {
                    message = response.message;
                }
                else
                {
                    message = "Something went Wrong";
                }
            }
            catch (Exception ex)
            { message = "Something went Wrong"; }
            return message;
        }

        public string MarkUserChapterAssociation(UserChapterAssociation.Request obj)
        {
            string message = "Failed";

            if (IsLogin == false)
            {
                message = "Login Required";
            }

            
            try
            {
                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                UserChapterAssociation.ResponsebyID response = new UserChapterAssociation.ResponsebyID();

                string UserChapterAssociationsAPI = AppSetting.BaseURL + AppSetting.UserChapterAssociationsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(UserChapterAssociationsAPI, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<UserChapterAssociation.ResponsebyID>(APIResponse);

                if (response.success == true)
                {
                    message = response.message;
                }
                else
                {
                    message = "Something went Wrong";
                }
            }
            catch (Exception ex)
            { message = "Something went Wrong"; }
            return message;
        }


        public IActionResult ChapterDetailView(int id, int course_id)
        {
            try
            {
                string message = "";
                UserChapterAssociation.Request request = new UserChapterAssociation.Request();
                request.course_id = course_id;
                request.chapter_id = id;
                message = MarkUserChapterAssociation(request);
            }
            catch (Exception ex)
            {
                
            }            

            return ViewComponent("ChapterDetailView", new { id = id, course_id = course_id });
        }



        #region API Calls   


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

        public CoursesModel.Response getCourses(int category_id)
        {
            CoursesModel.Response obj = new CoursesModel.Response();
            string qs = "?category_id=" + category_id.ToString();
            try
            {
                string CoursesAPIURL = AppSetting.BaseURL + AppSetting.CoursesAPI + qs;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CoursesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CoursesModel.Response>(APIResponse);
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


        #endregion

    }
}