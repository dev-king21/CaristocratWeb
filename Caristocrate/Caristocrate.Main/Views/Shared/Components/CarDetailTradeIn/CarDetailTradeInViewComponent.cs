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

namespace Caristocrate.Main.Views.Shared.Components.CarDetailTradeIn
{
    public class CarDetailTradeInViewComponent : ViewComponent
    {
        IDataProtector _dataProtector;
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public CarDetailTradeInViewComponent(IDataProtectionProvider provider, IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(string UserEmail, string UserName, string UserImage, string UserCountryCode, string UserPhoneNo)
        {
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            try
            {
                InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                getMakeobj = getMake();
                getModelobj = getModel();
                getCarTypeobj = GetcarTypes();
                regionalSpecificationModelObj = getRegionalSpecifications();
                CustMyCarsObj = GetCustomerCars();
            }
            catch (Exception ex)
            { }

            ViewData["GetMake"] = getMakeobj;
            ViewData["GetModel"] = getModelobj;
            ViewData["getCarTypeobj"] = getCarTypeobj;
            ViewData["regionalSpecificationModelObj"] = regionalSpecificationModelObj;
            ViewData["InteriorColorObj"] = InteriorColorObj;
            ViewData["ExteriorColorObj"] = ExteriorColorObj;
            ViewData["AccidentObj"] = AccidentObj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View();
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
    }
}
