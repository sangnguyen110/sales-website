using HoangViet.Areas.Admin.ViewModels;
using HoangViet.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HoangViet.Areas.Admin.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public abstract partial class BaseAdminController : Controller
    {
       
        /// <summary>
        /// Initialize controller
        /// </summary>
        /// <param name="requestContext">Request context</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
             base.Initialize(requestContext);
        }

        /// <summary>
        /// On exception
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    if (filterContext.ExceptionHandled)
        //    {
        //        return;
        //    }
        //    filterContext.Result = new ViewResult
        //    {
        //        ViewName = "~/Views/Shared/Error.cshtml"
        //    };
        //    filterContext.ExceptionHandled = true;
        //    base.OnException(filterContext);
        //}
        /// <summary>
        /// Save selected TAB index
        /// </summary>
        /// <param name="index">Idnex to save; null to automatically detect it</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected void SaveSelectedTabIndex(int? index = null, bool persistForTheNextRequest = true)
        {
            //keep this method synchronized with
            //"GetSelectedTabIndex" method of \Nop.Web.Framework\ViewEngines\Razor\WebViewPage.cs
            if (!index.HasValue)
            {
                int tmp = 0;
                if (int.TryParse(this.Request.Form["selected-tab-index"], out tmp))
                {
                    index = tmp;
                }
            }
            if (index.HasValue)
            {
                string dataKey = "hoangviet.selected-tab-index";
                if (persistForTheNextRequest)
                {
                    TempData[dataKey] = index;
                }
                else
                {
                    ViewData[dataKey] = index;
                }
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var layoutAdminViewModel = new LayoutAdminViewModel();
            layoutAdminViewModel.UserName = User.Identity.Name;
            layoutAdminViewModel.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            layoutAdminViewModel.Action = this.ControllerContext.RouteData.Values["action"].ToString(); 
            ViewBag.LayoutModel = layoutAdminViewModel;
            base.OnActionExecuting(filterContext);
        }
    }
}
