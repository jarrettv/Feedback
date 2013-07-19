using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aaa.Common
{
    public static class ControllerExtensions
    {
        public static ActionResult RedirectToLocal(this Controller controller, string returnUrl)
        {
            if (controller.Url.IsLocalUrl(returnUrl))
            {
                return new RedirectResult(returnUrl);
            }
            else
            {
                return new RedirectToRouteResult("Home", new System.Web.Routing.RouteValueDictionary());
            }
        }

        /// <summary>
        /// Sets the Response Status Code to 400 and suppresses default error handling by IIS.
        /// </summary>
        public static void SetResponseError400(this Controller controller)
        {
            controller.Response.StatusCode = 400;
            controller.Response.TrySkipIisCustomErrors = true;            
        }

        public static HttpStatusCodeResult DialogReloadEntirePage(this Controller controller)
        {
            return new HttpStatusCodeResult(205);
        }

        public static PartialViewResult DialogShowErrorView(this Controller controller, object model)
        {
            return controller.DialogShowErrorView(null, model);
        }

        public static PartialViewResult DialogShowErrorView(this Controller controller, string viewName, object model = null)
        {
            controller.SetResponseError400();
            controller.ViewData.Model = model;
            return new PartialViewResult
            {
                ViewName = viewName,
                ViewData = controller.ViewData,
                TempData = controller.TempData,
                ViewEngineCollection = controller.ViewEngineCollection
            };
        }
    }
}
