using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Caristocrate.Common;
using Caristocrate.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Caristocrate.Main.Controllers
{
    public class AccountController : Controller
    {
        private AppSettings AppSetting { get; set; }
        public AccountController(IOptions<AppSettings> settings)
        {
            AppSetting = settings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {           
            return View();
        }
        
        public string LoginPost(string email, string password)
        {
            string message = "Failed";
            try
            {
                LoginModel.Request request = new LoginModel.Request();
                request.email = email;
                request.password = password;
                request.device_type = "android";
                request.device_token = "";

                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                GetLoginDetails.Response obj = new GetLoginDetails.Response();

                string LoginAPI = AppSetting.BaseURL + AppSetting.LoginAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(LoginAPI, "POST", jsondata, "", 0);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetLoginDetails.Response>(APIResponse);

                if (obj.success == true)
                {
                    if (obj.data.user.details.is_verified == 1)
                    {
                        if (obj.data.user.status == 1)
                        {
                            List<Claim> listClaims = new List<Claim>();
                            listClaims.Add(new Claim(ClaimTypes.Hash, obj.data.user.access_token));
                            listClaims.Add(new Claim(ClaimTypes.Name, obj.data.user.name));
                            listClaims.Add(new Claim(ClaimTypes.PrimarySid, obj.data.user.id.ToString()));

                            var claimsIdentity = new ClaimsIdentity(
                                listClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            { };

                            HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties);


                            HttpContext.Session.SetString("UserID", obj.data.user.id.ToString());
                            HttpContext.Session.SetString("UserEmail", obj.data.user.email.ToString());
                            HttpContext.Session.SetString("UserName", obj.data.user.name.ToString());
                            HttpContext.Session.SetString("UserToken", obj.data.user.access_token.ToString());
                            HttpContext.Session.SetString("UserImage", obj.data.user.details.image_url.ToString());
                            HttpContext.Session.SetString("UserNotificationFlag", obj.data.user.push_notification.ToString());
                            HttpContext.Session.SetString("UserCountryCode", obj.data.user.details.country_code.ToString().Replace("+", ""));
                            HttpContext.Session.SetString("UserPhone", obj.data.user.details.phone == null ? "" : obj.data.user.details.phone.ToString());
                            HttpContext.Session.SetString("UserEnrolled", obj.data.user.details.is_enrolled == 0 ? "" : obj.data.user.details.is_enrolled.ToString());

                            int profilecompletionprogress = 0;
                            try
                            {
                                if (obj.data.user.name != null)
                                {
                                    if (obj.data.user.name != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.email != null)
                                {
                                    if (obj.data.user.email != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.details.country_code != null)
                                {
                                    if (obj.data.user.details.country_code != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.details.phone != null)
                                {
                                    if (obj.data.user.details.phone != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }

                                if (obj.data.user.details.nationality != null)
                                {
                                    if (obj.data.user.details.nationality != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.details.profession != null)
                                {
                                    if (obj.data.user.details.profession != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.details.image != null)
                                {
                                    if (obj.data.user.details.image != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.details.dob != null)
                                {
                                    if (obj.data.user.details.dob != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }
                                if (obj.data.user.details.about != null)
                                {
                                    if (obj.data.user.details.about != "")
                                    {
                                        profilecompletionprogress = profilecompletionprogress + 10;
                                    }
                                }

                                if (obj.data.user.details.gender > 0)
                                { profilecompletionprogress = profilecompletionprogress + 10; }
                            }
                            catch (Exception ex)
                            {

                            }
                            HttpContext.Session.SetString("UserProfileCompletion", profilecompletionprogress.ToString());


                            message = obj.message;
                        }
                        else
                        {
                            message = "User deactivated, please contact admin.";
                        }
                    }
                    else
                    {
                        message = "Email Verification Required";
                    }
                }
                else
                {
                    message = obj.message;                    
                }
            }
            catch (Exception ex)
            {
                message = "Incorrect Email Address or Password";                
            }
            return message;
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserToken");
            HttpContext.Session.Remove("UserImage");
            HttpContext.Session.Remove("UserNotificationFlag");
            HttpContext.Session.Remove("UserCountryCode");
            HttpContext.Session.Remove("UserPhone");
            HttpContext.Session.Remove("UserRegionIDs");            
            HttpContext.Session.Remove("UserProfileCompletion");
            HttpContext.Session.Remove("UserEnrolled");

            return RedirectToAction("Index","Home");
        }

        public IActionResult Register()
        {
            RegionsModel.Response getRegionsobj = new RegionsModel.Response();
            try
            {                
                getRegionsobj = getRegions(); 
                
            }
            catch (Exception ex)
            {
                
            }

            @ViewData["message"] = "";
            return View(getRegionsobj);
        }

        public IActionResult EmailVerficationfromLogin(string email)
        {
            string encryptionkey = AppSetting.EncryptionKey;
            string emailaddress = email;

            RegisterModel.ForgotPasswordResponse obj = new RegisterModel.ForgotPasswordResponse();
            string message = "";
            try
            {
                obj = ResendEmailVerificationCodeCall(email, 0);
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

            string querystring = General.EncryptString("email=" + emailaddress, encryptionkey);
            return RedirectToAction("EmailVerfication", "Account", new { data = querystring });            
        }

        public IActionResult EmailVerfication(string data)
        {
            string email = "";
            if (data != "")
            {
                string DecryptedQS = General.DecryptString(data, AppSetting.EncryptionKey);
                var QSParams = HttpUtility.ParseQueryString(DecryptedQS);
                email = QSParams.Get("email") != null ? QSParams.Get("email") : "";
            }
            ViewData["email"] = email;
            return View();
        }

        public string EmailVerficationSuccess(GetLoginDetails.Response obj)
        {
            string message = "Failed";
            if (obj.success == true)
            {
                if (obj.data.user.details.is_verified == 1)
                {
                    List<Claim> listClaims = new List<Claim>();
                    listClaims.Add(new Claim(ClaimTypes.Hash, obj.data.user.access_token));
                    listClaims.Add(new Claim(ClaimTypes.Name, obj.data.user.name));
                    listClaims.Add(new Claim(ClaimTypes.PrimarySid, obj.data.user.id.ToString()));

                    var claimsIdentity = new ClaimsIdentity(
                        listClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    { };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);


                    HttpContext.Session.SetString("UserID", obj.data.user.id.ToString());
                    HttpContext.Session.SetString("UserEmail", obj.data.user.email.ToString());
                    HttpContext.Session.SetString("UserName", obj.data.user.name.ToString());
                    HttpContext.Session.SetString("UserToken", obj.data.user.access_token.ToString());
                    HttpContext.Session.SetString("UserImage", obj.data.user.details.image_url.ToString());
                    HttpContext.Session.SetString("UserNotificationFlag", obj.data.user.push_notification.ToString());
                    HttpContext.Session.SetString("UserCountryCode", obj.data.user.details.country_code.ToString().Replace("+", ""));
                    HttpContext.Session.SetString("UserPhone", obj.data.user.details.phone == null ? "" : obj.data.user.details.phone.ToString());
                    HttpContext.Session.SetString("UserEnrolled", obj.data.user.details.is_enrolled == 0 ? "" : obj.data.user.details.is_enrolled.ToString());

                    int profilecompletionprogress = 0;
                    try
                    {
                        if (obj.data.user.name != null)
                        {
                            if (obj.data.user.name != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.email != null)
                        {
                            if (obj.data.user.email != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.details.country_code != null)
                        {
                            if (obj.data.user.details.country_code != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.details.phone != null)
                        {
                            if (obj.data.user.details.phone != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }

                        if (obj.data.user.details.nationality != null)
                        {
                            if (obj.data.user.details.nationality != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.details.profession != null)
                        {
                            if (obj.data.user.details.profession != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.details.image != null)
                        {
                            if (obj.data.user.details.image != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.details.dob != null)
                        {
                            if (obj.data.user.details.dob != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }
                        if (obj.data.user.details.about != null)
                        {
                            if (obj.data.user.details.about != "")
                            {
                                profilecompletionprogress = profilecompletionprogress + 10;
                            }
                        }

                        if (obj.data.user.details.gender > 0)
                        { profilecompletionprogress = profilecompletionprogress + 10; }
                    }
                    catch (Exception ex)
                    {

                    }
                    HttpContext.Session.SetString("UserProfileCompletion", profilecompletionprogress.ToString());


                    message = obj.message;
                }
                else
                {
                    message = "Email Verification Required";
                }
            }
            else
            {
                message = obj.message;
            }
            return message;
        }

        public string VerifyEmailVerficationCode(string email, int verifcationCode)
        {
            GetLoginDetails.Response obj = new GetLoginDetails.Response();
            string message = "";
            try
            {
                obj = VerifyEmailVerficationCodeCall(email, verifcationCode);
                if (obj.success == true)
                {
                    message = "Success";
                    message = EmailVerficationSuccess(obj);
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

        public string ResendEmailVerificationCode(string email, int isForResetPassword)
        {
            RegisterModel.ForgotPasswordResponse obj = new RegisterModel.ForgotPasswordResponse();
            string message = "";
            try
            {
                obj = ResendEmailVerificationCodeCall(email, isForResetPassword);
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

        [HttpPost]
        public IActionResult Register(IFormCollection form)
        {
            string message = "";
            RegisterModel.Request request = new RegisterModel.Request();
            RegisterModel.Response response = new RegisterModel.Response();

            try
            {
                request.name = form["txtFullName"].ToString();
                request.email = form["txtEmail"].ToString();
                request.password = form["txtPassword"].ToString();
                request.password_confirmation = form["txtConfirmPassword"].ToString();
                request.country_code = form["ddlPhoneCode"].ToString();
                request.phone = form["txtPhoneNo"].ToString();
                request.device_type = "android";
                request.region_id = Convert.ToInt32(form["ddlRegion"].ToString());

                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                string SignupAPI = AppSetting.BaseURL + AppSetting.SignupAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(SignupAPI, "POST", jsondata, "", 0);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterModel.Response>(APIResponse);

                if (response.success == true)
                {
                    message = "success";
                    string encryptionkey = AppSetting.EncryptionKey;
                    string emailaddress = request.email;
                    string querystring = General.EncryptString("email=" + emailaddress, encryptionkey);
                    return RedirectToAction("EmailVerfication", "Account", new { data = querystring });
                }
                else
                {
                    message = "Something went Wrong";
                }

            }
            catch (Exception ex)
            {
                message = "Something went Wrong";
            }

            ViewData["message"] = message;
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(IFormCollection form)
        {
            RegisterModel.ForgotPasswordResponse obj = new RegisterModel.ForgotPasswordResponse();
            try
            {
                string encryptionkey = AppSetting.EncryptionKey;
                string emailaddress = form["txtEmail"].ToString();
                obj = ResendEmailVerificationCodeCall(emailaddress, 1);
                string querystring = General.EncryptString("email=" + emailaddress, encryptionkey);
                return RedirectToAction("ResetPassword", "Account", new { data = querystring });
            }
            catch (Exception ex)
            {
                
            }
            return View();
        }

        public IActionResult ResetPassword(string data)
        {
            string email = "";
            if (data != "")
            {
                string DecryptedQS = General.DecryptString(data, AppSetting.EncryptionKey);
                var QSParams = HttpUtility.ParseQueryString(DecryptedQS);
                email = QSParams.Get("email") != null ? QSParams.Get("email") : "";
            }
            ViewData["email"] = email;
            ViewData["message"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(IFormCollection form)
        {
            string message = "";
            GetLoginDetails.Response obj = new GetLoginDetails.Response();
            string email = form["hdnVerificationEmail"].ToString();
            try
            {               
                string txtPassword = form["txtPassword"].ToString();
                string txtConfirmPassword = form["txtConfirmPassword"].ToString();
                string VCtxt1 = form["VCtxt1"].ToString();
                string VCtxt2 = form["VCtxt2"].ToString();
                string VCtxt3 = form["VCtxt3"].ToString();
                string VCtxt4 = form["VCtxt4"].ToString();
                string verificationcode = VCtxt1 + VCtxt2 + VCtxt3 + VCtxt4;
                int code = Convert.ToInt32(verificationcode);

                obj = ResetPasswordCall(email, code, txtPassword, txtConfirmPassword);

                if (obj.success == true)
                {
                    message = LoginPost(email, txtPassword);
                    if (message == "Logged in successfully")
                    {
                        return RedirectToAction("MyAccount", "Customer");
                    }
                }
                else
                {
                    message = "Wrong OTP entered or OTP has expired";
                }

            }
            catch (Exception ex)
            {
                
            }
            ViewData["email"] = email;
            ViewData["message"] = message;
            return View();
        }

        public RegionsModel.Response getRegions()
        {
            RegionsModel.Response obj = new RegionsModel.Response();
            try
            {
                string RegionsAPIURL = AppSetting.BaseURL + AppSetting.RegionsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(RegionsAPIURL, "GET", "", "Bearer ", 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RegionsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetLoginDetails.Response VerifyEmailVerficationCodeCall(string email, int verification_code)
        {
            string Request = "?email=" + email.ToString()
                            + "&verification_code=" + verification_code.ToString();

            GetLoginDetails.Response obj = new GetLoginDetails.Response();
            try
            {
                string VerifyResetCodeAPIURL = AppSetting.BaseURL + AppSetting.VerifyResetCodeAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(VerifyResetCodeAPIURL, "POST", "", "", 0);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetLoginDetails.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetLoginDetails.Response ResetPasswordCall(string email, int verification_code, string password, string confirmpassword)
        {
            string Request = "?email=" + email.ToString()
                            + "&verification_code=" + verification_code.ToString()
                            + "&password=" + password.ToString()
                            + "&password_confirmation=" + confirmpassword.ToString();

            GetLoginDetails.Response obj = new GetLoginDetails.Response();
            try
            {
                string ResetPasswordAPIURL = AppSetting.BaseURL + AppSetting.ResetPasswordAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ResetPasswordAPIURL, "POST", "", "", 0);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetLoginDetails.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public RegisterModel.ForgotPasswordResponse ResendEmailVerificationCodeCall(string email, int is_for_reset)
        {
            string Request = "?email=" + email.ToString()
                               + "&is_for_reset=" + is_for_reset.ToString();

            RegisterModel.ForgotPasswordResponse obj = new RegisterModel.ForgotPasswordResponse();
            try
            {
                string ForgetPasswordAPIURL = AppSetting.BaseURL + AppSetting.ForgetPasswordAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ForgetPasswordAPIURL, "GET", "", "", 0);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterModel.ForgotPasswordResponse>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}