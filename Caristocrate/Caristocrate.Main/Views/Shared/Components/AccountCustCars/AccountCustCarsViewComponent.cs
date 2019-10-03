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


namespace Caristocrate.Main.Views.Shared.Components.AccountCustCars
{
    public class AccountCustCarsViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public AccountCustCarsViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(string UserEmail)
        {
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();

            try
            {
                if (UserEmail != "")
                {
                    CustMyCarsObj = GetCustomerCars();
                }

            }
            catch (Exception ex)
            { }
            return View(CustMyCarsObj);
        }

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
    }
}
