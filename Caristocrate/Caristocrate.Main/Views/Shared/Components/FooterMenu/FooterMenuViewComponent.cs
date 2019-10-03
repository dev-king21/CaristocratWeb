using Caristocrate.Common;
using Caristocrate.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Caristocrate.Main.Views.Shared.Components.FooterMenu
{
    public class FooterMenuViewComponent : ViewComponent
    {
        private AppSettings AppSetting { get; set; }
        public string AuthAccessToken { get; set; }

        public FooterMenuViewComponent(IOptions<AppSettings> settings, IHttpContextAccessor httpcontext)
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

        public async Task<IViewComponentResult> InvokeAsync(CategoryModel.Response categories)
        {

            //CategoryModel.Response Categories = new CategoryModel.Response();
            try
            {
                if (categories == null)
                {
                    categories = GetBaseCategories();
                }
            }
            catch (Exception ex)
            { }
            return View(categories);
        }

        public CategoryModel.Response GetBaseCategories()
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
    }
}
