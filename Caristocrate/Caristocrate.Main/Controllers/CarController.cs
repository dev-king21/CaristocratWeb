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
using System.Text;
using System.IO;
using System.Net.Http.Headers;

namespace Caristocrate.Main.Controllers
{
    public class CarController : BaseController
    {
        IHttpContextAccessor thiscontext = null;
        private IHostingEnvironment _hostingEnvironment;
        
        public CarController(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext, IHostingEnvironment environment) : base(settings, httpcontext)
        {
            thiscontext = httpcontext;
            _hostingEnvironment = environment;
        }


        #region Cars and Details 

        [HttpGet]
        [Route("top-luxury-cars")]
        public IActionResult Index(string data = "", string isfromHome = "")
        {            
            GetCars.Request obj = new GetCars.Request();
            AdvertisementBannersModel.Response AdvertisementBannersobj = new AdvertisementBannersModel.Response();
            string BannerURL = "";
            string UserRegionIDs = "";

            if (UserEmail != "")
            {
                UserRegionIDs = thiscontext.HttpContext.Session.GetString("UserRegionIDs") != null ? thiscontext.HttpContext.Session.GetString("UserRegionIDs").ToString() : "";
                HttpContext.Session.SetString("UserRegionIDs", UserRegionIDs);                
            }

            UserRegionIDs = GetCookie("UserRegionIDs");
            if (UserRegionIDs != null)
            {
                if (UserRegionIDs != "")
                    SetCookie("UserRegionIDs", UserRegionIDs, 365);
            }
            else
            {
                UserRegionIDs = "";
            }

            try
            {
                if (data != "")
                {
                    string DecryptedQS = General.DecryptString(data, AppSetting.EncryptionKey);
                    var QSParams = HttpUtility.ParseQueryString(DecryptedQS);
                    obj.categoryID = QSParams.Get("categoryID") != null ? Convert.ToInt32(QSParams.Get("categoryID")) : 0;
                    obj.makeID = QSParams.Get("makeID") != null ? QSParams.Get("makeID") : "";
                    obj.modelID = QSParams.Get("modelID") != null ? QSParams.Get("modelID") : "";
                    obj.minPrice = QSParams.Get("minPrice") != null ? Convert.ToInt32(QSParams.Get("minPrice")) : 0; 
                    obj.maxPrice = QSParams.Get("maxPrice") != null ? Convert.ToInt32(QSParams.Get("maxPrice")) : 0; 
                    obj.minYear = QSParams.Get("minYear") != null ? Convert.ToInt32(QSParams.Get("minYear")) : 0; 
                    obj.maxYear = QSParams.Get("maxYear") != null ? Convert.ToInt32(QSParams.Get("maxYear")) : 0; 
                    obj.minKM = QSParams.Get("minKM") != null ? Convert.ToInt32(QSParams.Get("minKM")) : 0; 
                    obj.maxKM = QSParams.Get("maxKM") != null ? Convert.ToInt32(QSParams.Get("maxKM")) : 0; 
                    obj.CarType = QSParams.Get("CarType") != null ? QSParams.Get("CarType") : "";
                    obj.Region = QSParams.Get("Region") != null ? QSParams.Get("Region") : "";
                }

                CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
                categories.data = categories.data.Where(x => x.slug == "luxury-market").ToList();
                BannerURL = categories.data.FirstOrDefault().media.FirstOrDefault().file_url;
                ViewData["BannerURL"] = BannerURL;

                RegionsModel.Response getRegionsobj = new RegionsModel.Response();
                getRegionsobj = getRegions();
                ViewData["getRegionsobj"] = getRegionsobj;

                AdvertisementBannersobj = getAdvertisementBanners();
            }
            catch (Exception ex)
            { }

            ViewData["AdvertisementBannersobj"] = AdvertisementBannersobj;
            ViewData["UserRegionIDs"] = UserRegionIDs;
            ViewData["isfromHome"] = isfromHome;

            return View(obj);
        }
        
        [Route("~/top-luxury-cars/{brand}/{model}/{id}")]
        public IActionResult CarDetail(string brand, string model, string id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();            
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();                                    
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {
                list = getCarDetailsbySlug(id);
                int objectid = list.data.FirstOrDefault().id;
                obj = getCarDetails(objectid);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {                        
                        try
                        {                            
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                        
                    }

                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                            MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                            carrequest.car_id = obj.data.id;
                            carrequest.type = 10;
                            MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }

                int categoryID = obj.data.category.id;                
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                
            }
            catch (Exception ex)
            { }

            
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
           
            

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View(obj);
        }

        [Route("~/top-luxury/{brand}/{model}/{id}")]
        public IActionResult CarDetailbyID(string brand, string model, int id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            GetCars.Response GetCarsobj = new GetCars.Response();
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {
                
                obj = getCarDetails(id);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        CustMyCarsObj = GetCustomerCars();
                        try
                        {
                            InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                            ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                            AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                            getMakeobj = getMake();
                            getModelobj = getModel();
                            getCarTypeobj = GetcarTypes();
                            regionalSpecificationModelObj = getRegionalSpecifications();
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                        
                    }

                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                        MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                        carrequest.car_id = obj.data.id;
                        carrequest.type = 10;
                        MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }
                int categoryID = obj.data.category.id;
                GetCarsobj = getCars(categoryID);
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                Reviewsobj = getReviewsbyCarID(id);
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

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

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View("CarDetail", obj);
        }

        [Route("~/pre-owned-luxury/{brand}/{model}/{id}")]
        public IActionResult PreOwnedCarDetailbyID(string brand, string model, int id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            GetCars.Response GetCarsobj = new GetCars.Response();
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {

                obj = getCarDetails(id);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        CustMyCarsObj = GetCustomerCars();
                        try
                        {
                            InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                            ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                            AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                            getMakeobj = getMake();
                            getModelobj = getModel();
                            getCarTypeobj = GetcarTypes();
                            regionalSpecificationModelObj = getRegionalSpecifications();
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                       
                    }

                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                        MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                        carrequest.car_id = obj.data.id;
                        carrequest.type = 10;
                        MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }
                int categoryID = obj.data.category.id;
                GetCarsobj = getCars(categoryID);
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                Reviewsobj = getReviewsbyCarID(id);
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

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

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View("PreOwnedCarDetail", obj);
        }

        [Route("~/top-luxury-cars2/{brand}/{model}/{id}")]
        public IActionResult CarDetailNew(string brand, string model, string id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            GetCars.Response GetCarsobj = new GetCars.Response();
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {
                list = getCars(28);

                int ObjectID = list.data.Where(a => a.slug == id).FirstOrDefault().id;
                obj = getCarDetails(ObjectID);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        CustMyCarsObj = GetCustomerCars();
                        try
                        {
                            InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                            ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                            AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                            getMakeobj = getMake();
                            getModelobj = getModel();
                            getCarTypeobj = GetcarTypes();
                            regionalSpecificationModelObj = getRegionalSpecifications();
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                        
                    }
                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                        MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                        carrequest.car_id = obj.data.id;
                        carrequest.type = 10;
                        MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }
                int categoryID = obj.data.category.id;
                GetCarsobj = getCars(categoryID);
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                Reviewsobj = getReviewsbyCarID(ObjectID);
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

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

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View(obj);
        }

        [Route("~/pre-owned-luxury-cars/{brand}/{model}/{id}")]
        public IActionResult PreOwnedCarDetail(string id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            GetCars.Response GetCarsobj = new GetCars.Response();
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {
                list = getCars(0);

                int ObjectID = list.data.Where(a => a.slug == id).FirstOrDefault().id;
                obj = getCarDetails(ObjectID);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        CustMyCarsObj = GetCustomerCars();
                        try
                        {
                            InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                            ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                            AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                            getMakeobj = getMake();
                            getModelobj = getModel();
                            getCarTypeobj = GetcarTypes();
                            regionalSpecificationModelObj = getRegionalSpecifications();
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                        
                    }
                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                        MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                        carrequest.car_id = obj.data.id;
                        carrequest.type = 10;
                        MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }
                int categoryID = obj.data.category.id;
                GetCarsobj = getCars(categoryID);
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                Reviewsobj = getReviewsbyCarID(ObjectID);
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

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

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View(obj);
        }

        [Route("~/new-luxury-cars/{brand}/{model}/{id}")]
        public IActionResult OutletMall(string id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            GetCars.Response GetCarsobj = new GetCars.Response();
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {
                list = getCars(0);

                int ObjectID = list.data.Where(a => a.slug == id).FirstOrDefault().id;
                obj = getCarDetails(ObjectID);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        CustMyCarsObj = GetCustomerCars();
                        try
                        {
                            InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                            ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                            AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                            getMakeobj = getMake();
                            getModelobj = getModel();
                            getCarTypeobj = GetcarTypes();
                            regionalSpecificationModelObj = getRegionalSpecifications();
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                        
                    }
                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                        MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                        carrequest.car_id = obj.data.id;
                        carrequest.type = 10;
                        MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }
                int categoryID = obj.data.category.id;
                GetCarsobj = getCars(categoryID);
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                Reviewsobj = getReviewsbyCarID(ObjectID);
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

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

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View(obj);
        }

        [Route("~/classic-cars/{brand}/{model}/{id}")]
        public IActionResult ClassicCars(string id)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            GetCars.Response GetCarsobj = new GetCars.Response();
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            BankRateModel.Response BankRateModelobj = new BankRateModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            ReviewsModel.Response Reviewsobj = new ReviewsModel.Response();
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            GetMake.Response getMakeobj = new GetMake.Response();
            GetModel.Response getModelobj = new GetModel.Response();
            CarType.Response getCarTypeobj = new CarType.Response();
            RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
            ReportCategoriesModel.Response reportCategoriesObj = new ReportCategoriesModel.Response();

            try
            {
                list = getCars(0);

                int ObjectID = list.data.Where(a => a.slug == id).FirstOrDefault().id;
                obj = getCarDetails(ObjectID);

                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        CustMyCarsObj = GetCustomerCars();
                        try
                        {
                            InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                            ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                            AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                            getMakeobj = getMake();
                            getModelobj = getModel();
                            getCarTypeobj = GetcarTypes();
                            regionalSpecificationModelObj = getRegionalSpecifications();
                            reportCategoriesObj = getReportCategories();
                        }
                        catch (Exception ex)
                        { }                        
                    }
                    if (obj.data != null)
                    {
                        //if (obj.data.is_viewed == false)
                        //{
                        MarkFavouriteModel.CarRequest carrequest = new MarkFavouriteModel.CarRequest();
                        carrequest.car_id = obj.data.id;
                        carrequest.type = 10;
                        MarkFavouriteObj = MarkFavourite(1, null, carrequest);
                        //}
                    }
                }
                int categoryID = obj.data.category.id;
                GetCarsobj = getCars(categoryID);
                SettingModelobj = getSettings();
                BankRateModelobj = getBankRate();
                Reviewsobj = getReviewsbyCarID(ObjectID);
            }
            catch (Exception ex)
            { }

            ViewData["GetCarsobj"] = GetCarsobj;
            ViewData["SettingModelobj"] = SettingModelobj;
            ViewData["BankRateModelobj"] = BankRateModelobj;
            ViewData["Reviewsobj"] = Reviewsobj;
            ViewData["CustMyCarsObj"] = CustMyCarsObj;

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

            ViewData["reportCategoriesObj"] = reportCategoriesObj;
            ViewData["SiteBaseURL"] = AppSetting.SiteBaseURL;

            return View(obj);
        }

        public string MarkFavouriteCar(MarkFavouriteModel.CarRequest carrequest)
        {
            MarkFavouriteModel.Response response = new MarkFavouriteModel.Response();
            string message = "";
            try
            {
                response = MarkFavourite(1, null, carrequest);
                if (response.success == true)
                {
                    message = "success";
                }
            }
            catch (Exception ex) { }
            return message;
        }

        public bool SetRegionSession(string regionids)
        {
            bool flag = false;
            //HttpContext.Session.SetString("UserRegionIDs", regionids.ToString());
            SetCookie("UserRegionIDs", regionids, 365);
            return flag;
        }

        public string PostCarReport(CarReportModel.Request request)
        {
            CarReportModel.Response response = new CarReportModel.Response();
            string message = "";
            try
            {
                response = PostCarReportCall(request);
                if (response.success == true)
                {
                    message = "success";
                }
            }
            catch (Exception ex) { }
            return message;
        }
        
        #endregion


        #region Compare Car 

        [Route("compare-car-specs")]
        public IActionResult CompareCar()
        {
            try
            {
                GetMake.Response getMakeobj = new GetMake.Response();
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;

                GetModel.Response getModelobj = new GetModel.Response();
                getModelobj = getModel();
                ViewData["GetModel"] = getModelobj;

                //GetCars.Response getCarsobj = new GetCars.Response();
                //getCarsobj = getCarsbyLimit(28, 20, 0);
                //ViewData["getCarsobj"] = getCarsobj;

                AdvertisementBannersModel.Response AdvertisementBannersobj = new AdvertisementBannersModel.Response();
                AdvertisementBannersobj = getAdvertisementBanners();
                ViewData["AdvertisementBannersobj"] = AdvertisementBannersobj;
            }
            catch (Exception ex)
            {
            }

            return View();
        }
        [Route("~/compare-car-specs/{carIds}")]
        public IActionResult CompareCarResult(string carIds)
        {            
            GetCars.Response obj = new GetCars.Response();
            string IDs = "";
            try
            {                
                //obj = getCars(28);
                //foreach (var item in obj.data)
                //{
                    foreach (string slug in carIds.Split(","))
                    {
                        obj = getCarDetailsbySlug(slug);
                        int objectid = obj.data.FirstOrDefault().id;
                        IDs = IDs + objectid + ",";
                    }
                //}
                IDs = IDs.Substring(0, IDs.Length - 1);

                obj = new GetCars.Response();
                obj = getCars(0, IDs);
            }
            catch (Exception ex)
            { }

            return View(obj);
        }
        public IActionResult CompareCarResultTemp(string carIds)
        {
            
            GetCars.Response obj = new GetCars.Response();            
            try
            {
                carIds = carIds.Replace("vs", ",");                
            }
            catch (Exception ex)
            {
                
            }
                        
            return RedirectToAction("CompareCarResult", new { carIds = carIds });
        }
        [Route("car-comparison-tool")]
        public IActionResult CompareSegments()
        {
            AdvertisementBannersModel.Response AdvertisementBannersobj = new AdvertisementBannersModel.Response();
            CarType.Response CarTypeobj = new CarType.Response();

            try
            {
                CarTypeobj = GetcarTypes();
                AdvertisementBannersobj = getAdvertisementBanners();
            }
            catch (Exception ex)
            {
            }

            ViewData["ValidationMessage"] = TempData["ValidationMessage"];
            ViewData["AdvertisementBannersobj"] = AdvertisementBannersobj;
            return View(CarTypeobj);
        }
        [Route("~/car-comparison-tool/{id}")]
        public IActionResult CompareSegmentResult(string id)
        {
            CarType.Response carttype = new CarType.Response();
            GetCars.Response obj = new GetCars.Response();
            try
            {
                carttype = GetcarTypes();
                int objectID = carttype.data.Where(a => a.slug == id).FirstOrDefault().id;
                obj = getCars(28, "", objectID.ToString());
            }
            catch (Exception ex)
            { }
            return View(obj);
        }

        public int CheckCompareSegmentCarCount(int id)
        {
            int count = 0;
            GetCars.Response obj = new GetCars.Response();
            try
            {
                obj = getCars(28, "", id.ToString());
                if (obj != null)
                {
                    if (obj.data != null)
                    {                        
                        count = obj.data.Count;                        
                    }
                }
            }
            catch (Exception ex)
            { }
            return count;
        }

        public string getVersionbyYear(int categoryID, string makeid = "", string modelid = "", int minyear = 0, int masyear = 0)
        {
            List<GetCarsforCompareCar> list = new List<GetCarsforCompareCar>();
            GetCars.Response obj = new GetCars.Response();
            try
            {
                obj = getVersionsbyYear(categoryID, makeid, modelid, minyear, masyear);
                if (obj.success == true)
                {
                    foreach (var item in obj.data)
                    {
                        GetCarsforCompareCar car = new GetCarsforCompareCar();
                        car.Version = item.version == null ? "" : item.version.name;
                        car.carid = item.id;
                        car.slug = item.slug;
                        car.imagePath = item.media.FirstOrDefault().file_url;
                        list.Add(car);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            string jsondata = "";
            if (list != null)
            {
                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            }

            return jsondata;
        }

        public string GetYearsbyMakeModel(int categoryID, string makeid = "", string modelid = "")
        {
            List<int> list = new List<int>();
            GetCars.Response obj = new GetCars.Response();
            try
            {
                obj = getYearsbyMakeModelCall(categoryID, makeid, modelid);
                if (obj.success == true)
                {
                    foreach (var item in obj.data)
                    {                        
                        list.Add(item.year);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            string jsondata = "";
            if (list != null)
            {
                list = list.Distinct().ToList();
                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            }

            return jsondata;
        }
        
        #endregion


        #region Ask for Consultancy

        public string PersonalShopperRequest(ContactUsModel.Request obj)
        {
            string message = "Failed";
            try {
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
        [Route("car-expert")]
        public IActionResult AskForConsultancy()
        {
            SettingModel.Response SettingModelobj = new SettingModel.Response();
            try
            {
                SettingModelobj = getSettings();
            }
            catch (Exception ex)
            { }
            ViewData["Message"] = "";

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View(SettingModelobj);
        }

        [HttpPost]
        [Route("car-expert")]
        public IActionResult AskForConsultancy(IFormCollection form)
        {

            ContactUsModel.RequestWithoutCarID request = new ContactUsModel.RequestWithoutCarID();
            ContactUsModel.Response response = new ContactUsModel.Response();
            string message = "Failed";
            try
            {
                request.name = form["txtName"].ToString();
                request.email = form["txtEmail"].ToString();
                request.country_code = form["txtPhoneCode"].ToString();
                request.phone = form["txtPhoneNo"].ToString();
                request.type = Convert.ToInt32(form["hdnConsultancyType"].ToString());
                

                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                string ContactusAPI = AppSetting.BaseURL + AppSetting.ContactusAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ContactusAPI, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactUsModel.Response>(APIResponse);

                if (response.success == true)
                {
                    ViewData["Message"] = "Request Submitted Successfully";
                    message = response.message;
                }
                else
                {
                    ViewData["Message"] = response.message;
                    message = "Something went Wrong";
                }

            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Something went wrong";
            }

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View();
        }

        #endregion


        #region News / Women Only, Auto Life and Car Education

        
        [Route("auto-life")]
        public IActionResult News()
        {

            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            categories.data = categories.data.Where(a => a.slug == "auto-life").ToList();

            NewsModel.Response obj = new NewsModel.Response();
            int categoryID = categories.data.FirstOrDefault().id;
            string categoryName = categories.data.FirstOrDefault().name;
            int requestid = 0;

            try {                
                obj = GetNews(requestid);                
            }
            catch (Exception ex) {
            }

            ViewData["requestid"] = requestid;
            ViewData["categoryID"] = categoryID;
            ViewData["categoryName"] = categoryName;
            ViewData["categories"] = categories;
            ViewData["slug"] = "auto-life";
            return View(obj);
        }

        [Route("car-advice")]
        public IActionResult CarEducation()
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            categories.data = categories.data.Where(a => a.slug == "careducation").ToList();

            NewsModel.Response obj = new NewsModel.Response();
            int categoryID = categories.data.FirstOrDefault().id;
            string categoryName = categories.data.FirstOrDefault().name;
            int requestid = categories.data.FirstOrDefault().id;

            try
            {
                obj = GetNews(requestid);
            }
            catch (Exception ex)
            {
            }

            ViewData["requestid"] = requestid;
            ViewData["categoryID"] = categoryID;
            ViewData["categoryName"] = categoryName;
            ViewData["categories"] = categories;
            ViewData["slug"] = "careducation";

            return View("News", obj);
        }

        [Route("women-in-automotive")]
        public IActionResult WomenOnly()
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            categories.data = categories.data.Where(a => a.slug == "for-women-only").ToList();

            NewsModel.Response obj = new NewsModel.Response();
            int categoryID = categories.data.FirstOrDefault().id;
            string categoryName = categories.data.FirstOrDefault().name;
            int requestid = categories.data.FirstOrDefault().id;

            try
            {
                obj = GetNews(requestid);
            }
            catch (Exception ex)
            {
            }

            ViewData["requestid"] = requestid;
            ViewData["categoryID"] = categoryID;
            ViewData["categoryName"] = categoryName;
            ViewData["categories"] = categories;
            ViewData["slug"] = "for-women-only";

            return View("News", obj);
        }

        [Route("~/auto-life/{slug}")]
        public IActionResult AutolifeChild(string slug = "")
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            categories.data = categories.data.Where(a => a.slug == slug).ToList();

            NewsModel.Response obj = new NewsModel.Response();
            int categoryID = categories.data.FirstOrDefault().id;
            string categoryName = categories.data.FirstOrDefault().name;
            int requestid = categoryID;

            try
            {
                obj = GetNews(requestid);
            }
            catch (Exception ex)
            {
            }

            ViewData["requestid"] = 0;
            ViewData["categoryID"] = categoryID;
            ViewData["categoryName"] = categoryName;
            ViewData["categories"] = categories;
            ViewData["slug"] = slug;

            return View("News", obj);
        }

        [Route("~/auto-life/{slug}/{id}")]
        public IActionResult NewsbyID(string slug, string id = "")
        {
            NewsModel.Response NewsList = new NewsModel.Response();
            NewsbyIDModel.Response obj = new NewsbyIDModel.Response();
            NewsModel.Response Newsobj = new NewsModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            try
            {
                NewsList = GetNews(0);
                int objtectID = NewsList.data.Where(a => a.slug == id).FirstOrDefault().id;

                obj = GetNewsbyID(objtectID);
                Newsobj = GetNews(obj.data.category_id);


                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        if (obj.data != null)
                        {
                            if (obj.data.is_viewed == false)
                            {
                                MarkFavouriteModel.NewsRequest newsrequest = new MarkFavouriteModel.NewsRequest();
                                newsrequest.news_id = obj.data.id;
                                newsrequest.type = 10;
                                MarkFavouriteObj = MarkFavourite(2, newsrequest, null);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }

            ViewData["Newsobj"] = Newsobj;

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View(obj);
        }

        [Route("~/car-advice/{id}")]
        public IActionResult CarEducationbyID(string id = "")
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            categories.data = categories.data.Where(a => a.slug == "careducation").ToList();

            NewsModel.Response NewsList = new NewsModel.Response();
            NewsbyIDModel.Response obj = new NewsbyIDModel.Response();
            NewsModel.Response Newsobj = new NewsModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            try
            {
                NewsList = GetNews(categories.data.FirstOrDefault().id);
                int objtectID = NewsList.data.Where(a => a.slug == id).FirstOrDefault().id;

                obj = GetNewsbyID(objtectID);
                Newsobj = GetNews(obj.data.category_id);


                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        if (obj.data != null)
                        {
                            if (obj.data.is_viewed == false)
                            {
                                MarkFavouriteModel.NewsRequest newsrequest = new MarkFavouriteModel.NewsRequest();
                                newsrequest.news_id = obj.data.id;
                                newsrequest.type = 10;
                                MarkFavouriteObj = MarkFavourite(2, newsrequest, null);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }

            ViewData["Newsobj"] = Newsobj;

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View("NewsbyID", obj);
        }

        [Route("~/women-in-automotive/{id}")]
        public IActionResult WomenOnlybyID(string id = "")
        {
            CategoryModel.Response categories = ViewData["categories"] as CategoryModel.Response;
            categories.data = categories.data.Where(a => a.slug == "for-women-only").ToList();

            NewsModel.Response NewsList = new NewsModel.Response();
            NewsbyIDModel.Response obj = new NewsbyIDModel.Response();
            NewsModel.Response Newsobj = new NewsModel.Response();
            MarkFavouriteModel.Response MarkFavouriteObj = new MarkFavouriteModel.Response();
            try
            {
                NewsList = GetNews(categories.data.FirstOrDefault().id);
                int objtectID = NewsList.data.Where(a => a.slug == id).FirstOrDefault().id;

                obj = GetNewsbyID(objtectID);
                Newsobj = GetNews(obj.data.category_id);


                if (obj.success == true)
                {
                    if (UserEmail != "")
                    {
                        if (obj.data != null)
                        {
                            if (obj.data.is_viewed == false)
                            {
                                MarkFavouriteModel.NewsRequest newsrequest = new MarkFavouriteModel.NewsRequest();
                                newsrequest.news_id = obj.data.id;
                                newsrequest.type = 10;
                                MarkFavouriteObj = MarkFavourite(2, newsrequest, null);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }

            ViewData["Newsobj"] = Newsobj;

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            return View("NewsbyID", obj);
        }

        public string MarkFavouriteNews(MarkFavouriteModel.NewsRequest newsrequest)
        {
            MarkFavouriteModel.Response response = new MarkFavouriteModel.Response();
            string message = "";
            try {
                response = MarkFavourite(2, newsrequest, null);
                if (response.success == true)
                {
                    message = "success";
                }
            }
            catch (Exception ex) { }
            return message;
        }

        public string SubmitNewsComments(CommentsModel.Request request)
        {
            CommentsModel.ResponsebyID response = new CommentsModel.ResponsebyID();
            string message = "";
            try
            {
                response = SubmiteNewsCommentsCall(request);
                if (response.success == true)
                {
                    message = "success";
                }
            }
            catch (Exception ex)
            {
            }
            return message;
        }

        #endregion


        #region Car Reviews
        [Route("carbuyer-reviews")]
        public IActionResult Reviews()
        {
            GetCars.Response obj = new GetCars.Response();
            try {

                GetMake.Response getMakeobj = new GetMake.Response();
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;

                GetModel.Response getModelobj = new GetModel.Response();
                getModelobj = getModel();
                ViewData["GetModel"] = getModelobj;

                CarType.Response getCarTypeobj = new CarType.Response();
                getCarTypeobj = GetcarTypes();
                ViewData["getCarTypeobj"] = getCarTypeobj;

                obj = GetCarsforReview(28, -1, 10, 0);


                ViewData["MakeID"] = "";
                ViewData["ModelID"] = "";
                ViewData["MinimumRating"] = "";
                ViewData["CarTypeID"] = "";
                ViewData["MinAmount"] = "";
                ViewData["MaxAmount"] = "";
            }
            catch (Exception ex)
            { }
            return View(obj);
        }

        [HttpPost]
        [Route("carbuyer-reviews")]
        public IActionResult Reviews(IFormCollection form)
        {
            GetCars.Response obj = new GetCars.Response();
            string makeid = "";
            string modelid = "";
            string cartypeid = "";
            string minimumrating = "";
            string minbudget = "";
            string maxbudget = "";
            try {
                makeid = form["ddlMake"].ToString();
                modelid = form["ddlModel"].ToString();
                minimumrating = form["ddlMinimumRating"].ToString();
                cartypeid = form["ddlCarType"].ToString();
                minbudget = form["hdnamount"].ToString().Split('-')[0];
                maxbudget = form["hdnamount"].ToString().Split('-')[1];

                ViewData["MakeID"] = form["ddlMake"].ToString();
                ViewData["ModelID"] = form["ddlModel"].ToString();
                ViewData["MinimumRating"] = form["ddlMinimumRating"].ToString();
                ViewData["CarTypeID"] = form["ddlCarType"].ToString();
                ViewData["MinAmount"] = form["hdnamount"].ToString().Split('-')[0];
                ViewData["MaxAmount"] = form["hdnamount"].ToString().Split('-')[1];

                if (minbudget == "1000")
                    minbudget = "";
                if (maxbudget == "5000000")
                    maxbudget = "";

                obj = getReviewedCars(28, makeid, modelid, cartypeid, minimumrating, minbudget, maxbudget, 1);

                GetMake.Response getMakeobj = new GetMake.Response();
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;

                GetModel.Response getModelobj = new GetModel.Response();
                getModelobj = getModel();
                ViewData["GetModel"] = getModelobj;

                CarType.Response getCarTypeobj = new CarType.Response();
                getCarTypeobj = GetcarTypes();
                ViewData["getCarTypeobj"] = getCarTypeobj;
            }
            catch (Exception ex) { }
            
            return View(obj);
        }

        [Route("~/carbuyer-reviews/{brand}/{model}/{carid}")]
        public IActionResult ReviewsbyID(string brand, string model, string carid)
        {
            GetCars.Response list = new GetCars.Response();
            GetCarDetail.Response obj = new GetCarDetail.Response();
            ReviewAspects.Response REviewAspects = new ReviewAspects.Response();
            try
            {
                list = getCarDetailsbySlug(carid);
                //int objectid = list.data.FirstOrDefault().id;
                //obj = getCarDetails(objectid);

                //list = getCars(28);

                int ObjectID = list.data.FirstOrDefault().id;
                obj = getCarDetails(ObjectID);
                REviewAspects = getReviewAspects();
            }
            catch (Exception ex)
            {
             
            }

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

            ViewData["REviewAspects"] = REviewAspects;
            return View(obj);
        }

        public string PostReview(string request)
        {
            string message = "";
            ReviewsModel.ResponsebyID obj = new ReviewsModel.ResponsebyID();
            try
            {
                obj = PostReviewCall(request);
                if (obj.success == true)
                {
                    message = "success";
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

        #endregion


        #region Car Evaluation

        [Route("used-car-valuation")]
        public IActionResult EvaluateCar()
        {
            CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
            CarAttributesModel.Response InteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response ExteriorColorObj = new CarAttributesModel.Response();
            CarAttributesModel.Response AccidentObj = new CarAttributesModel.Response();
            try
            {
                if (UserEmail != "")
                {
                    CustMyCarsObj = GetCustomerCars();
                    InteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.INTERIOR_COLOR_KEY));
                    ExteriorColorObj = GetCarAttributes(Convert.ToInt32(AppSetting.EXTERIOR_COLOR_KEY));
                    AccidentObj = GetCarAttributes(Convert.ToInt32(AppSetting.ACCIDENT_KEY));
                }

                GetMake.Response getMakeobj = new GetMake.Response();
                getMakeobj = getMake();
                ViewData["GetMake"] = getMakeobj;

                GetModel.Response getModelobj = new GetModel.Response();
                getModelobj = getModel();
                ViewData["GetModel"] = getModelobj;

                CarType.Response getCarTypeobj = new CarType.Response();
                getCarTypeobj = GetcarTypes();
                ViewData["getCarTypeobj"] = getCarTypeobj;

                RegionalSpecificationModel.Response regionalSpecificationModelObj = new RegionalSpecificationModel.Response();
                regionalSpecificationModelObj = getRegionalSpecifications();
                ViewData["regionalSpecificationModelObj"] = regionalSpecificationModelObj;

                AdvertisementBannersModel.Response AdvertisementBannersobj = new AdvertisementBannersModel.Response();
                AdvertisementBannersobj = getAdvertisementBanners();
                ViewData["AdvertisementBannersobj"] = AdvertisementBannersobj;
            }
            catch (Exception ex)
            {
            }

            ViewData["UserName"] = UserName;
            ViewData["UserImage"] = UserImage;
            ViewData["UserEmail"] = UserEmail;
            ViewData["UserCountryCode"] = UserCountryCode;
            ViewData["UserPhoneNo"] = UserPhoneNo;

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

        [HttpPost]
        public async Task<string> AddMyCar(IFormFile front, IFormFile Back, IFormFile Right, IFormFile Left, IFormFile Interior, IFormFile RegCard,
            string username, string email, string countrycode, string phone, int makeid, int modelid, int year, string version_app, int regionalSpecid, string KM,
            string Chassis, int exteriorcolorid, int interiorcolorid, int accidentid, string warrantyrem, string servicerem, string notes, int ownerid, decimal amount, int type, string car_attributes)
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


                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AuthAccessToken));

                    var response = await client.PostAsync(AppSetting.BaseURL + AppSetting.CustmyCarsAPI, formData);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    else
                    {
                        message = "Success";
                        CustMyCars.Response CustMyCarsObj = new CustMyCars.Response();
                        CustMyCarsObj = GetCustomerCars();

                        if (CustMyCarsObj.success == true)
                        {
                            int? customercarid = CustMyCarsObj.data.FirstOrDefault().id;

                            CustTradeInModel.Request request = new CustTradeInModel.Request();
                            CustTradeInModel.RequestEvaluation requestevaluation = new CustTradeInModel.RequestEvaluation();

                            if (type == 20)
                            {
                                requestevaluation.customer_car_id = customercarid;
                                requestevaluation.amount = amount;
                                requestevaluation.type = type;
                                requestevaluation.notes = notes;
                                message = EvaluationRequest(requestevaluation);
                            }
                            else
                            {
                                request.customer_car_id = customercarid;
                                request.amount = amount;
                                request.type = type;
                                request.notes = notes;
                                request.owner_car_id = ownerid;                                
                                message = TradeInRequest(request);
                            }                            

                            
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return message;
        }

        public string TradeInRequest(CustTradeInModel.Request request)
        {
            string message = "";
            CustTradeInByIDModel.Response response = new CustTradeInByIDModel.Response();
            try
            {
                response = PostTradeInRequest(request);
                if (response.success = true)
                {
                    message = "Success";
                }
            }
            catch (Exception rx)
            {
            }
            return message;
        }

        public string EvaluationRequest(CustTradeInModel.RequestEvaluation request)
        {
            string message = "";
            CustTradeInByIDModel.Response response = new CustTradeInByIDModel.Response();
            try
            {
                response = PostEvaluationRequest(request);
                if (response.success = true)
                {
                    message = "Success";
                }
            }
            catch (Exception rx)
            {
            }
            return message;
        }

        #endregion


        #region Vin Check
        [Route("vin-number-decoder")]
        public IActionResult VinCheck()
        {
            try
            {
                AdvertisementBannersModel.Response AdvertisementBannersobj = new AdvertisementBannersModel.Response();
                AdvertisementBannersobj = getAdvertisementBanners();
                ViewData["AdvertisementBannersobj"] = AdvertisementBannersobj;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        #endregion


        #region Mark Banner Viewed

        public string MarkBannerViewPost(AdvertisementBannersModel.Request request)
        {

            AdvertisementBannersModel.InteractionResponse response = new AdvertisementBannersModel.InteractionResponse();
            string message = "";
            try
            {
                response = MarkBannerView(request);
                if (response.success == true)
                {
                    message = "success";
                }
            }
            catch (Exception ex) { }
            return message;

        }

        #endregion

        


        #region ViewComponent Calls

        public IActionResult LeftFilter(GetCars.Request obj = null)
        {
            return ViewComponent("LeftFilter", new { obj = obj });
        }

        public IActionResult FilterByBrand()
        {
            return ViewComponent("FilterByBrand");
        }

        public IActionResult CustCars()
        {
            return ViewComponent("CustCars", new { UserEmail = UserEmail });
        }

        public IActionResult CarDetailTradeIn()
        {            
            return ViewComponent("CarDetailTradeIn", new { UserEmail = UserEmail, UserName = UserName, UserImage= UserImage, UserCountryCode = UserCountryCode, UserPhoneNo = UserPhoneNo });
        }

        public IActionResult MostReviewedCars()
        {
            return ViewComponent("MostReviewedCars");
        }

        public IActionResult CarsforReview(int categoryID, string brand_ids, string model_ids,
            string car_type = "", string rating = "",
            string min_price = "", string max_price = "",
            int sortID = 2, int limit = 10, int offset = 0)
        {
            return ViewComponent("CarsforReview", new {
                categoryID = categoryID,
                brand_ids = brand_ids == null ? "" : brand_ids,
                model_ids = model_ids == null ? "" : model_ids,
                car_type = car_type == null ? "" : car_type,
                rating = rating == null ? "" : rating,
                min_price = min_price == null ? "" : min_price,
                max_price = max_price == null ? "" : max_price,
                sortID = sortID,
                limit = limit,
                offset = offset });
        }

        public IActionResult CarDetailSimilarListing(int categoryID = 0, int carID = 0, string brand = "", string model = "", string slug = "")
        {
            return ViewComponent("CarDetailSimilarListing", new { categoryID = categoryID, carID = carID, brand = brand, model = model, slug = slug });
        }

        public IActionResult CarDetailUserReviews(int carID = 0, string brand = "", string model = "", string slug = "", decimal average_rating = 0)
        {
            return ViewComponent("CarDetailUserReviews", new { carID = carID, brand = brand, model = model, slug = slug, average_rating = average_rating });
        }

        public IActionResult FilteredCars(GetCars.Request obj = null)
        {
            return ViewComponent("FilteredCars", new { obj = obj });
        }

        public IActionResult NewsComments(int newsID = 0)
        {
            return ViewComponent("NewsComments", new { newsID = newsID });
        }

        public IActionResult CarReviews(int carID = 0)
        {
            return ViewComponent("CarReviews", new { carID = carID });
        }

        public IActionResult ModelSearch(string ModelName = "", string makeID = "",
            int minPrice = 0, int maxPrice = 0, int minYear = 0, int maxYear = 0)
        {
            return ViewComponent("ModelSearch", new
            {
                ModelName = ModelName,
                makeID = makeID,
                minPrice = minPrice,
                maxPrice = maxPrice,
                minYear = minYear,
                maxYear = maxYear
            });
        }        

        #endregion







        #region API Calls     

        public AdvertisementBannersModel.InteractionResponse MarkBannerView(AdvertisementBannersModel.Request request)
        {
            AdvertisementBannersModel.InteractionResponse response = new AdvertisementBannersModel.InteractionResponse();
            string jsondata = "";
            string apistring = "";
            try
            {
                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                apistring = AppSetting.BannerInteractionsAPI;

                string InteractionsAPIURL = AppSetting.BaseURL + apistring;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(InteractionsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<AdvertisementBannersModel.InteractionResponse>(APIResponse);

            }
            catch (Exception ex) { }
            return response;
        }

        public AdvertisementBannersModel.Response getAdvertisementBanners()
        {
            AdvertisementBannersModel.Response obj = new AdvertisementBannersModel.Response();
            try
            {
                string AdvertisementBannersAPIURL = AppSetting.BaseURL + AppSetting.AdvertisementBannersAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(AdvertisementBannersAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<AdvertisementBannersModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CustTradeInByIDModel.Response PostTradeInRequest(CustTradeInModel.Request request)
        {
            CustTradeInByIDModel.Response response = new CustTradeInByIDModel.Response();
            string jsondata = "";
            string apistring = "";
            try
            {
                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                apistring = AppSetting.CustTradeInAPI;
                string InteractionsAPIURL = AppSetting.BaseURL + apistring;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(InteractionsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<CustTradeInByIDModel.Response>(APIResponse);
            }
            catch (Exception ex) { }
            return response;
        }

        public CustTradeInByIDModel.Response PostEvaluationRequest(CustTradeInModel.RequestEvaluation request)
        {
            CustTradeInByIDModel.Response response = new CustTradeInByIDModel.Response();
            string jsondata = "";
            string apistring = "";
            try
            {
                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                apistring = AppSetting.CustTradeInAPI;
                string InteractionsAPIURL = AppSetting.BaseURL + apistring;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(InteractionsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<CustTradeInByIDModel.Response>(APIResponse);
            }
            catch (Exception ex) { }
            return response;
        }

        public CustMyCars.ResponsebyID AddCarPost(string formdata)
        {
            CustMyCars.ResponsebyID obj = new CustMyCars.ResponsebyID();
            try
            {
                string CustmyCarsAPIURL = AppSetting.BaseURL + AppSetting.CustmyCarsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CustmyCarsAPIURL, "POST", formdata, AuthAccessToken, 1, true);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CustMyCars.ResponsebyID>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }        

        public VersionsModel.Response getVersions(int modelid)
        {
            string qs = "?model_id=" + modelid.ToString();
            VersionsModel.Response obj = new VersionsModel.Response();
            try
            {
                string CarVersionsAPIURL = AppSetting.BaseURL + AppSetting.CarVersionsAPI + qs;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(CarVersionsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<VersionsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getYearsbyMakeModelCall(int categoryID, string makeid = "", string modelid = "")
        {

            string Request = "?category_id=" + categoryID.ToString()
                            + "&brand_ids=" + makeid.ToString()
                            + "&model_ids=" + modelid.ToString();

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

        public GetCars.Response getVersionsbyYear(int categoryID, string makeid = "", string modelid = "", int minyear = 0, int masyear = 0, int sort_by_version = 1)
        {

            string Request = "?category_id=" + categoryID.ToString()
                            + "&brand_ids=" + makeid.ToString()
                            + "&model_ids=" + modelid.ToString()
                            + "&min_year=" + minyear.ToString()
                            + "&max_year=" + masyear.ToString()
                            + "&sort_by_version=" + sort_by_version.ToString();

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

        public GetCars.Response getCarsbyYearandVersionCall(int categoryID, string makeid = "", string modelid = "", int minyear = 0, int masyear = 0, int versionID = 0)
        {

            string Request = "?category_id=" + categoryID.ToString()
                            + "&brand_ids=" + makeid.ToString()
                            + "&model_ids=" + modelid.ToString()
                            + "&min_year=" + minyear.ToString()
                            + "&max_year=" + masyear.ToString()
                            + "&version_id=" + versionID.ToString();

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

        public ReviewsModel.ResponsebyID PostReviewCall(string jsondata)
        {
            ReviewsModel.ResponsebyID response = new ReviewsModel.ResponsebyID();
            try
            {
                //string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                string ReviewsAPIURL = AppSetting.BaseURL + AppSetting.ReviewsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ReviewsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<ReviewsModel.ResponsebyID>(APIResponse);
            }
            catch (Exception ex)
            {      
            }
            return response;
        }

        public MarkFavouriteModel.Response MarkFavourite(int type, MarkFavouriteModel.NewsRequest newsrequest, MarkFavouriteModel.CarRequest carrequest)
        {
            MarkFavouriteModel.Response response = new MarkFavouriteModel.Response();
            string jsondata = "";
            string apistring = "";
            try
            {
                if (type == 1)
                {
                    jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(carrequest);
                    apistring = AppSetting.CarInteractionsAPI;
                }
                else
                {
                    jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(newsrequest);
                    apistring = AppSetting.NewsInteractionsAPI;
                }

                string InteractionsAPIURL = AppSetting.BaseURL + apistring;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(InteractionsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<MarkFavouriteModel.Response>(APIResponse);

            }
            catch (Exception ex) { }
            return response;
        }

        public CommentsModel.ResponsebyID SubmiteNewsCommentsCall(CommentsModel.Request request)
        {
            CommentsModel.ResponsebyID response = new CommentsModel.ResponsebyID();
            try
            {
                string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                string InteractionsAPIURL = AppSetting.BaseURL + AppSetting.CommentsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(InteractionsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentsModel.ResponsebyID>(APIResponse);

            }
            catch (Exception ex) { }
            return response;
        }

        public GetCarDetail.Response getCarDetails(int id)
        {
            GetCarDetail.Response obj = new GetCarDetail.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + "/" + id;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);                
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCarDetail.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getCarDetailsbySlug(string slug)
        {
            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPIV2 + "?slug=" + slug;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getCars(int categoryID, string CarIDs = "", string cartype = "", int is_for_review = 0)
        {
            string Request = "?category_id=" + categoryID.ToString()
                            + "&car_ids=" + CarIDs.ToString()
                            + "&car_sub_type=" + cartype.ToString()
                            + "&is_for_review=" + is_for_review.ToString();

            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response GetCarsforReview(int categoryID, int is_for_review = -1, int limit = 10, int offset = 0)
        {
            string Request = "?category_id=" + categoryID.ToString()
                               + "&limit=" + limit + "&offset=" + offset
                               + "&sort_by_rating=" + is_for_review.ToString();
            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getSimilarListingCars(int categoryID)
        {
            string Request = "?category_id=" + categoryID.ToString()
                            + "&limit=10&offset=0";

            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getCarsbyLimit(int categoryID, int limit, int offset)
        {
            string Request = "?category_id=" + categoryID.ToString() + "&limit=" + limit + "&offset=" + offset;

            GetCars.Response obj = new GetCars.Response();
            try
            {
                string GetFilteredCarsAPI = AppSetting.BaseURL + AppSetting.GetFilteredCarsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(GetFilteredCarsAPI, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                APIResponse = APIResponse.Replace("Transmission ", "Transmission");
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCars.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public GetCars.Response getReviewedCars(int categoryID, string brand_ids, string model_ids, string car_type = "", string rating = "", string min_price = "", string max_price = "", int is_for_review = -1)
        {

            string Request = "?category_id=" + categoryID.ToString()
                            + "&brand_ids=" + brand_ids.ToString()
                            + "&model_ids=" + model_ids.ToString()
                            + "&car_type=" + car_type.ToString()
                            + "&rating=" + rating.ToString()
                            + "&min_price=" + min_price.ToString()
                            + "&max_price=" + max_price.ToString()
                            + "&sort_by_rating=" + is_for_review.ToString()
                            + "&limit=10&offset=0";

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

        public ReviewAspects.Response getReviewAspects()
        {
            ReviewAspects.Response obj = new ReviewAspects.Response();
            try
            {
                string ReviewAspectsAPIURL = AppSetting.BaseURL + AppSetting.ReviewAspectsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ReviewAspectsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReviewAspects.Response>(APIResponse);
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

        public ReportCategoriesModel.Response getReportCategories()
        {
            ReportCategoriesModel.Response obj = new ReportCategoriesModel.Response();
            try
            {
                string ReportCategoriesAPIURL = AppSetting.BaseURL + AppSetting.ReportCategoriesAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ReportCategoriesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportCategoriesModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CarReportModel.Response PostCarReportCall(CarReportModel.Request request)
        {
            CarReportModel.Response response = new CarReportModel.Response();
            string jsondata = "";
            string apistring = "";
            try
            {
                jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                apistring = AppSetting.carReportsAPI;

                string InteractionsAPIURL = AppSetting.BaseURL + apistring;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(InteractionsAPIURL, "POST", jsondata, AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<CarReportModel.Response>(APIResponse);

            }
            catch (Exception ex) { }
            return response;
        }

        public SettingModel.Response getSettings()
        {
            SettingModel.Response obj = new SettingModel.Response();
            try
            {
                string settingsAPIURL = AppSetting.BaseURL + AppSetting.settingsAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(settingsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public BankRateModel.Response getBankRate()
        {
            BankRateModel.Response obj = new BankRateModel.Response();
            try
            {
                string BanksRatesAPIURL = AppSetting.BaseURL + AppSetting.BanksRatesAPI;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(BanksRatesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<BankRateModel.Response>(APIResponse);
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

        public CarType.Response GetSubSegments(int parentID)
        {
            CarType.Response obj = new CarType.Response();
            try
            {
                string carTypesAPIURL = AppSetting.BaseURL + AppSetting.carTypesAPI + "?parent_id=" + parentID.ToString();
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(carTypesAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CarType.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public NewsModel.Response GetNews(int categoryID)
        {
            NewsModel.Response obj = new NewsModel.Response();
            try
            {
                string request = "?category_id=" + categoryID;
                if (categoryID == 0)
                {
                    request = "";
                }

                string NewsAPIURL = AppSetting.BaseURL + AppSetting.NewsAPI + request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(NewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public NewsbyIDModel.Response GetNewsbyID(int id)
        {
            NewsbyIDModel.Response obj = new NewsbyIDModel.Response();
            try
            {
                string NewsAPIURL = AppSetting.BaseURL + AppSetting.NewsAPI + "/" + id;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(NewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsbyIDModel.Response>(APIResponse);
            }
            catch (Exception ex)
            { }
            return obj;
        }

        public CategoryModel.Response GetCategories()
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

        public ReviewsModel.Response getReviewsbyCarID(int car_id)
        {
            string Request = "?car_id=" + car_id.ToString();

            ReviewsModel.Response obj = new ReviewsModel.Response();

            try
            {
                string ReviewsAPIURL = AppSetting.BaseURL + AppSetting.ReviewsAPI + Request;
                string APIResponse = "";
                APIWebRequest objAPI = new APIWebRequest(ReviewsAPIURL, "GET", "", AuthAccessToken, 1);
                APIResponse = objAPI.GetResponse();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReviewsModel.Response>(APIResponse);
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


        #region Cookie Methods

        //public string GetCookie(string key)
        //{
        //    return Request.Cookies[key];
        //}

        //public void SetCookie(string key, string value, int? expireTime)
        //{
        //    CookieOptions option = new CookieOptions();
        //    if (expireTime.HasValue)
        //        option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        //    else
        //        option.Expires = DateTime.Now.AddMilliseconds(10);
        //    Response.Cookies.Append(key, value, option);
        //}

        //public void RemoveCookie(string key)
        //{
        //    Response.Cookies.Delete(key);
        //}

        #endregion
    }
}