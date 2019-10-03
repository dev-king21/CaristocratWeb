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
namespace Caristocrate.Main.Views.Shared.Components.CustEditMyCar
{
    public class CustEditMyCarViewComponent : ViewComponent
    {
        
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public CustEditMyCarViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(int id, string UserName, string UserImage, string UserEmail, string UserCountryCode, string UserPhoneNo)
        {
            CustMyCars.ResponsebyID CustMyCarsObj = new CustMyCars.ResponsebyID();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();

            try
            {
                CustMyCarsObj = GetCustomerCars(id);
                InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));                
                getMakeobj = getMake();                              
                getModelobj = getModel();                               
                getCarTypeobj = GetcarTypes();                               
                regionalSpecificationModelObj = getRegionalSpecifications();               
            }
            catch (Exception ex)
            { }

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

        public CustMyCars.ResponsebyID GetCustomerCars(int id)
        {
            CustMyCars.ResponsebyID obj = new CustMyCars.ResponsebyID();
            try
            {
                string CustmyCarsAPIURL = AppSetting.BaseURL + AppSetting.CustmyCarsAPI + "/" + id.ToString();
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustmyCarsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustMyCars.ResponsebyID>(APIResponse);
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
    }
}
