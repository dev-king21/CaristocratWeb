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
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

namespace Caristocrate.Main.Controllers
{
    public class CustomerController : Controller
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
        private IHostingEnvironment _hostingEnvironment;

        public CustomerController(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext, IHostingEnvironment environment)
        {            
            _hostingEnvironment = environment;
            AppSetting = settings.Value;

            try
            {
                UserName = "";
                UserImage = "";
                UserEmail = "";
                UserCountryCode = "";
                UserPhoneNo = "";

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
                        UserNotificationFlag = Convert.ToInt32(httpcontext.HttpContext.Session.GetString("UserNotificationFlag").ToString());
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            CustProfileModel.Response obj = new CustProfileModel.Response();
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }

                obj = GetCustomerProfile();

                int profilecompletionprogress = 0;
                try
                {
                    if (obj.data.name != null)
                    {
                        if (obj.data.name != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.email != null)
                    {
                        if (obj.data.email != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.details.country_code != null)
                    {
                        if (obj.data.details.country_code != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.details.phone != null)
                    {
                        if (obj.data.details.phone != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                   
                    if (obj.data.details.nationality != null)
                    {
                        if (obj.data.details.nationality != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.details.profession != null)
                    {
                        if (obj.data.details.profession != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.details.image != null)
                    {
                        if (obj.data.details.image != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.details.dob != null)
                    {
                        if (obj.data.details.dob != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }
                    if (obj.data.details.about != null)
                    {
                        if (obj.data.details.about != "")
                        {
                            profilecompletionprogress = profilecompletionprogress + 10;
                        }
                    }

                    if (obj.data.details.gender > 0)
                    { profilecompletionprogress = profilecompletionprogress + 10; }
                }
                catch (Exception ex)
                {

                    throw;
                }
                HttpContext.Session.SetString("UserProfileCompletion", profilecompletionprogress.ToString());
                HttpContext.Session.SetString("UserName", obj.data.name.ToString());                
                HttpContext.Session.SetString("UserImage", obj.data.details.image_url.ToString());
                HttpContext.Session.SetString("UserCountryCode", obj.data.details.country_code.ToString().Replace("+", ""));
                HttpContext.Session.SetString("UserPhone", obj.data.details.phone == null ? "" : obj.data.details.phone.ToString());
            }
            catch (Exception ex)
            {

            }

            return View(obj);
        }

        public IActionResult Notification()
        {
            CustNotificationModel.Response obj = new CustNotificationModel.Response();
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }

                obj = GetCustomerNotifications();
            }
            catch (Exception ex)
            {

            }

            return View(obj);
        }

        public IActionResult MyTradeIns(string data = "")
        {
            CustTradeInModel.Response obj = new CustTradeInModel.Response();
            int categoryID = 0;

            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }
               
                if (data != "")
                {
                    string DecryptedQS = General.DecryptString(data, AppSetting.EncryptionKey);
                    var QSParams = HttpUtility.ParseQueryString(DecryptedQS);
                    categoryID = QSParams.Get("categoryID") != null ? Convert.ToInt32(QSParams.Get("categoryID")) : 0;
                }

                obj = GetCustomerTradeIn(0, 10);
            }
            catch (Exception ex)
            {

            }

            ViewData["categoryID"] = categoryID;

            return View(obj);
        }

        public IActionResult MyEvaluations()
        {
            CustTradeInModel.Response obj = new CustTradeInModel.Response();
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }

                obj = GetCustomerTradeIn(0, 20);
            }
            catch (Exception ex)
            {

            }

            return View(obj);
        }

        public IActionResult MyEvaluationbyID(int id)
        {
            CustTradeInByIDModel.Response obj = new CustTradeInByIDModel.Response();
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }

                obj = GetCustomerTradeInbyID(id, 0);
            }
            catch (Exception ex)
            {

            }

            return View(obj);
        }

        public IActionResult MyTradeInsbyID(int id)
        {
            CustTradeInByIDModel.Response obj = new CustTradeInByIDModel.Response();
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }

                obj = GetCustomerTradeInbyID(id, 0);
            }
            catch (Exception ex)
            {

            }

            return View(obj);
        }

        public IActionResult ChangePassword()
        {
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }               
            }
            catch (Exception ex)
            {

            }
            //ViewData["APIURL"] = AppSetting.BaseURL + AppSetting.CustChangePasswordAPI;
            ViewData["Message"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(IFormCollection form)
        {
            string OldPassword = form["txtOldPassword"].ToString();
            string NewPassword = form["txtNewPassword"].ToString();
            string ConfirmPassword = form["txtConfirmPassword"].ToString();

            string formdata = "old_password=" + OldPassword + "&password=" + NewPassword + "&password_confirmation=" + ConfirmPassword;
            UpdateChangePassword(formdata);
            ViewData["Message"] = "success";
            return View();
        }

        public IActionResult Setting()
        {
            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {

            }
            ViewData["UserNotificationFlag"] = UserNotificationFlag;
            return View();
        }

        public int UpdateCustomerPushNotificationFlag(int flag)
        {
            UpdatePushNotificationFlagModel.Response obj = new UpdatePushNotificationFlagModel.Response();
            obj = UpdatePushNotificationFlag(flag);

            if (obj.success == true)
            {
                HttpContext.Session.Remove("UserNotificationFlag");
                HttpContext.Session.SetString("UserNotificationFlag", obj.data.push_notification.ToString());
            }

            return obj.data.push_notification;
        }

        public IActionResult MyAccount()
        {
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            GetCars.Response CustFavCarsObj = new GetCars.Response();
            CustProfileModel.Response profileobj = new CustProfileModel.Response();
            CustNews.Response custnewsObj = new CustNews.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();

            try
            {
                if (IsLogin == false)
                {
                    return RedirectToAction("Index", "Home");
                }

                CustFavCarsObj = GetCustomerFavouriteCars();                
                profileobj = GetCustomerProfile();
                custnewsObj = GetCustomerFavouriteNews();
                getMakeobj = getMake();
                getModelobj = getModel();
                getCarTypeobj = GetcarTypes();
                regionalSpecificationModelObj = getRegionalSpecifications();
                InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
            }
            catch (Exception ex)
            {

            }

            ViewData["CustFavCarsObj"] = CustFavCarsObj;
            ViewData["profileobj"] = profileobj;
            ViewData["custnewsObj"] = custnewsObj;

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            ViewData["GetMake"] = getMakeobj;
            ViewData["GetModel"] = getModelobj;
            ViewData["getCarTypeobj"] = getCarTypeobj;
            ViewData["regionalSpecificationModelObj"] = regionalSpecificationModelObj;
            ViewData["InteriorColorObj"] = InteriorColorObj;
            ViewData["ExteriorColorObj"] = ExteriorColorObj;
            ViewData["AccidentObj"] = AccidentObj;

            return View(CustMyCarsObj);
        }

        public string FileUpload(IFormFile file)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string filePath = "";
            if (file.Length > 0)
            {
                filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return filePath;
        }

        public async Task<string> DeactivateAccount(int status)
        {
            string message = "";
            try
            {
                StreamContent streamContent = null;
                HttpContent stringContent = null;


                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {

                    stringContent = new StringContent(status.ToString());
                    formData.Add(stringContent, "status");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AuthAccessToken));

                    var response = await client.PostAsync(AppSetting.BaseURL + AppSetting.UpdateprofileAPI, formData);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    else
                    {
                        message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return message;
        }
    

        [HttpPost]
        public async Task<string> UpdateProfile(IFormFile image, string name, string country_code, string phone,
           string dob, string nationality, string profession, string about, int gender)
        {
            //CustMyCars.ResponsebyID response = new CustMyCars.ResponsebyID();

            string message = "";
            try
            {
                StreamContent streamContent = null;
                HttpContent stringContent = null;


                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    if (name != null)
                    {
                        stringContent = new StringContent(name);
                        formData.Add(stringContent, "name");
                    }

                    if (country_code != null)
                    {
                        stringContent = new StringContent(country_code);
                        formData.Add(stringContent, "country_code");
                    }

                    if (phone != null)
                    {
                        stringContent = new StringContent(phone);
                        formData.Add(stringContent, "phone");
                    }

                    if (dob != null)
                    {
                        stringContent = new StringContent(dob);
                        formData.Add(stringContent, "dob");
                    }

                    if (nationality != null)
                    {
                        stringContent = new StringContent(nationality);
                        formData.Add(stringContent, "nationality");
                    }

                    if (profession != null)
                    {
                        stringContent = new StringContent(profession);
                        formData.Add(stringContent, "profession");
                    }

                    if (gender != 0)
                    {
                        stringContent = new StringContent(gender.ToString());
                        formData.Add(stringContent, "gender");
                    }

                    if (about != null)
                    {
                        stringContent = new StringContent(about);
                        formData.Add(stringContent, "about");
                    }

                   
                    if (image != null)
                    {
                        try
                        {
                            string filepath = FileUpload(image);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "image", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                   

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AuthAccessToken));

                    var response = await client.PostAsync(AppSetting.BaseURL + AppSetting.UpdateprofileAPI, formData);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    else
                    {
                        message = "Success";                        
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return message;
        }


        public IActionResult AccountCustCars()
        {
            return ViewComponent("AccountCustCars", new { UserEmail = UserEmail });
        }

        public IActionResult CustEditMyCar(int id)
        {
            return ViewComponent("CustEditMyCar", new {
                id = id,
                UserName = UserName,
                UserImage = UserImage,
                UserEmail = UserEmail,
                UserCountryCode = UserCountryCode,
                UserPhoneNo = UserPhoneNo
            });
        }

        [HttpPost]
        public async Task<string> AddMyCarfromAccountArea(IFormFile front, IFormFile Back, IFormFile Right, IFormFile Left, IFormFile Interior, IFormFile RegCard,
            string username, string email, string countrycode, string phone, int makeid, int modelid, int year, string version_app, int regionalSpecid, string KM,
            string Chassis, int exteriorcolorid, int interiorcolorid, int accidentid, string warrantyrem, string servicerem, string notes, string car_attributes, 
            int isUpdate = 0, string deleted_images = "", int carid = 0)
        {
            //CustMyCars.ResponsebyID response = new CustMyCars.ResponsebyID();

            string message = "";
            try
            {
                StreamContent streamContent = null;
                HttpContent stringContent = null;


                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    stringContent = new StringContent(username);
                    formData.Add(stringContent, "name");

                    stringContent = new StringContent(email);
                    formData.Add(stringContent, "email");

                    stringContent = new StringContent(countrycode);
                    formData.Add(stringContent, "country_code");

                    stringContent = new StringContent(phone);
                    formData.Add(stringContent, "phone");

                    if (Chassis != null)
                    {
                        stringContent = new StringContent(Chassis);
                        formData.Add(stringContent, "chassis");
                    }

                    if (KM != null)
                    {
                        stringContent = new StringContent(KM);
                        formData.Add(stringContent, "kilometer");
                    }

                    if (version_app != null)
                    {
                        stringContent = new StringContent(version_app);
                        formData.Add(stringContent, "version_app");
                    }

                    stringContent = new StringContent(modelid.ToString());
                    formData.Add(stringContent, "model_id");

                    stringContent = new StringContent(year.ToString());
                    formData.Add(stringContent, "year");

                    stringContent = new StringContent(regionalSpecid.ToString());
                    formData.Add(stringContent, "regional_specification_id");

                    if (notes != null)
                    {
                        stringContent = new StringContent(notes);
                        formData.Add(stringContent, "notes");
                    }

                    if (front != null)
                    {
                        try
                        {
                            string filepath = FileUpload(front);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "media[front]", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    if (Back != null)
                    {
                        try
                        {
                            string filepath = FileUpload(Back);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "media[back]", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    if (Right != null)
                    {
                        try
                        {
                            string filepath = FileUpload(Right);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "media[right]", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    if (Left != null)
                    {
                        try
                        {
                            string filepath = FileUpload(Left);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "media[left]", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    if (Interior != null)
                    {
                        try
                        {
                            string filepath = FileUpload(Interior);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "media[interior]", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    if (RegCard != null)
                    {
                        try
                        {
                            string filepath = FileUpload(RegCard);
                            FileStream fs = System.IO.File.OpenRead(filepath);
                            streamContent = new StreamContent(fs);
                            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            formData.Add(imageContent, "media[registration_card]", Path.GetFileName(filepath));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    try
                    {
                        stringContent = new StringContent(car_attributes.ToString());
                        formData.Add(stringContent, "car_attributes");

                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        if (isUpdate == 1)
                        {
                            if (deleted_images != null)
                            {
                                if (deleted_images != "")
                                {
                                    stringContent = new StringContent(deleted_images);
                                    formData.Add(stringContent, "deleted_images", deleted_images);
                                }
                            }

                            stringContent = new StringContent("PUT");
                            formData.Add(stringContent, "_method");
                        }
                    }

                    catch (Exception ex)
                    { }

                    string APIURL = AppSetting.BaseURL + AppSetting.CustmyCarsAPI;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AuthAccessToken));
                    
                    if (isUpdate == 1)
                    {
                        APIURL = AppSetting.BaseURL + AppSetting.CustmyCarsAPI + "/" + carid;                        
                    }

                    var response = await client.PostAsync(APIURL, formData);

                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    else
                    {
                        message = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return message;
        }

        #region API Calls

        public CustMyCars.Response GetCustomerCars()
        {
            CustMyCars.Response obj = new CustMyCars.Response();
            try
            {
                string CustmyCarsAPIURL = AppSetting.BaseURL + AppSetting.CustmyCarsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustmyCarsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustMyCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response GetCustomerFavouriteCars()
        {
            string Request = "?favorite=1";

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

        public CustNews.Response GetCustomerFavouriteNews()
        {
            CustNews.Response obj = new CustNews.Response();
            try
            {
                string CustfavoriteNewsAPIURL = AppSetting.BaseURL + AppSetting.CustfavoriteNewsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustfavoriteNewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustNews.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CustProfileModel.Response GetCustomerProfile()
        {
            CustProfileModel.Response obj = new CustProfileModel.Response();
            try
            {
                string CustProfileAPIURL = AppSetting.BaseURL + AppSetting.CustProfileAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustProfileAPIURL, "POST", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustProfileModel.Response>(APIResponse);
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
                string CustNotificationAPIURL = AppSetting.BaseURL + AppSetting.CustNotificationAPI + "?is_read=1";
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustNotificationAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustNotificationModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CustProfileModel.Response UpdateChangePassword(string formdata)
        {
            CustProfileModel.Response obj = new CustProfileModel.Response();
            try
            {
                string CustChangePasswordAPIURL = AppSetting.BaseURL + AppSetting.CustChangePasswordAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustChangePasswordAPIURL, "POST", formdata, AuthAccessToken, 1, true);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustProfileModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public UpdatePushNotificationFlagModel.Response UpdatePushNotificationFlag(int Flag)
        {
            UpdatePushNotificationFlagModel.Response obj = new UpdatePushNotificationFlagModel.Response();
            try
            {
                string CustomerUpdatePushNotificationAPIURL = AppSetting.BaseURL + AppSetting.CustomerUpdatePushNotificationAPI + "?push_notification=" + Flag.ToString();
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustomerUpdatePushNotificationAPIURL, "POST", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdatePushNotificationFlagModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CustTradeInModel.Response GetCustomerTradeIn(int id = 0, int type = 10)
        {

            string Parameters = "";

            if (id == 0)
            {
                Parameters = "?type=" + type.ToString();
            }
            else
            {
                Parameters = "/" + id.ToString();
            }

            CustTradeInModel.Response obj = new CustTradeInModel.Response();
            try
            {
                string CustTradeInAPIURL = AppSetting.BaseURL + AppSetting.CustTradeInAPI + Parameters;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustTradeInAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustTradeInModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CustTradeInByIDModel.Response GetCustomerTradeInbyID(int id = 0, int type = 10)
        {

            string Parameters = "";

            if (id == 0)
            {
                Parameters = "?type=" + type.ToString();
            }
            else
            {
                Parameters = "/" + id.ToString();
            }

            CustTradeInByIDModel.Response obj = new CustTradeInByIDModel.Response();
            try
            {
                string CustTradeInAPIURL = AppSetting.BaseURL + AppSetting.CustTradeInAPI + Parameters;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustTradeInAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustTradeInByIDModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetMake.Response getMake()
        {
            GetMake.Response obj = new GetMake.Response();
            try
            {
                string GetMakeAPIURL = AppSetting.BaseURL + AppSetting.GetBrandsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetMakeAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMake.Response>(APIResponse);
                obj.data = obj.data.OrderBy(a => a.name).ToList();
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public RegionalSpecificationModel.Response getRegionalSpecifications()
        {
            RegionalSpecificationModel.Response obj = new RegionalSpecificationModel.Response();
            try
            {
                string RegionalSpecificationsAPIURL = AppSetting.BaseURL + AppSetting.RegionalSpecificationsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(RegionalSpecificationsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RegionalSpecificationModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CarType.Response GetcarTypes()
        {
            CarType.Response obj = new CarType.Response();
            try
            {
                string carTypesAPIURL = AppSetting.BaseURL + AppSetting.carTypesAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(carTypesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CarType.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetModel.Response getModel()
        {
            GetModel.Response obj = new GetModel.Response();
            try
            {
                string GetMakeAPIURL = AppSetting.BaseURL + AppSetting.GetModelsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetMakeAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CarAttributesModel.Response GetCarAttributes(int id)
        {
            CarAttributesModel.Response obj = new CarAttributesModel.Response();
            try
            {
                string CarAttributesAPIURL = AppSetting.BaseURL + AppSetting.CarAttributesAPI + "/" + id;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CarAttributesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CarAttributesModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        #endregion
    }
}