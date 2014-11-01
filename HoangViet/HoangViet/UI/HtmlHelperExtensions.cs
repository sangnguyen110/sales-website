using System.Web.Mvc;

namespace HoangViet.UI
{
    public static class HtmlHelperExtensions
    {
        public static string ActivePage(this HtmlHelper helper, string area, string controller = null)
        {
            string classValue = "";

            string currentArea = helper.ViewContext.RouteData.DataTokens["area"].ToString().ToLower();
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString().ToLower();
           // string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString().ToLower();
            if (string.IsNullOrEmpty(controller) && currentArea == area
                || currentArea == area && currentController == controller)
                
                    classValue = "active";
               

            return classValue;
        }
    }
}