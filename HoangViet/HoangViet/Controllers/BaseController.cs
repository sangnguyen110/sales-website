using HoangViet.UI;
using HoangViet.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Linq;
using HoangViet.Models.Orders;
using HoangViet.Models.Accounts;

namespace HoangViet.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        private HoangVietUserManager _userManager;
        public HoangVietUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HoangVietUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private HoangVietRoleManager _roleManager;
        public HoangVietRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HoangVietRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public LayoutViewModel LayoutModel { get; set; }
        protected HoangViet.Models.HoangVietDbContext Db { get; set; }

        /// <summary>
        /// Initialize controller
        /// </summary>
        /// <param name="requestContext">Request context</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Db = requestContext.HttpContext.GetOwinContext().Get<HoangViet.Models.HoangVietDbContext>();
            LayoutModel = new LayoutViewModel();

            base.Initialize(requestContext);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            //Original source code: http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");

            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }


        /// <summary>
        /// Display success notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }
        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }
        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        /// <param name="logException">A value indicating whether exception should be logged</param>
        protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, exception.Message, persistForTheNextRequest);
        }
        /// <summary>
        /// Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("nop.notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            PopulateDataForLayout();
            ViewBag.LayoutModel = this.LayoutModel;
            base.OnActionExecuting(filterContext);
        }

        protected void PopulateDataForLayout()
        {
            this.LayoutModel.TopLevelCategory = new SelectList(Db.Categories.Where(c => c.ParentCategory == null), "Id", "CategoryName");
            this.LayoutModel.IsAuthenticated = this.Request.IsAuthenticated;
            this.LayoutModel.IsAdmin = this.User.IsInRole("Admin");
            this.LayoutModel.UserName = this.User.Identity.Name == "" ? "Khách hàng" : this.User.Identity.Name;
            this.LayoutModel.MenuItems = this.Db.Categories
                .Where(c => c.Published && c.ShowOnHomePage && c.ParentCategory == null)
                .OrderByDescending(c => c.DisplayOrder).ThenByDescending(c => c.Id).ToList().Select(c => new MenuItem
                {
                    Title = c.CategoryName,
                    Url = c.GetUrl(),
                    CountProduct = c.Products.Count,
                    Slug = c.Slug,
                    ChildMenuItems = c.ChildCategories
                    .Where(d => d.Published && d.ShowOnHomePage)
                    .OrderByDescending(d => d.DisplayOrder).ThenByDescending(d => d.Id).Select(d => new MenuItem
                    {
                        Title = d.CategoryName,
                        Url = d.GetUrl(),
                        Slug = c.Slug,
                        CountProduct = d.Products.Count
                    }).ToList()
                }).ToList();
            if (this.RouteData.Values["category"] != null)
                this.LayoutModel.SelectedMenuItem = this.RouteData.Values["category"].ToString();

            //get cart
            var cart = new ShoppingCart(this.HttpContext, Db);
            LayoutModel.CartCount = cart.GetCount();
            LayoutModel.CartTotal = cart.GetTotal();

        }
    }
}
