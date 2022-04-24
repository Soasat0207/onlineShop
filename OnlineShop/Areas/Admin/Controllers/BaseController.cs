using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers //base của tất cả các trontroller khác
{
    public class BaseController : Controller
    {
        //initilizing culture on controller initialization //đa ngôn ngữ
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonConstants.CurrentCulture] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }
        // changing culture //đa ngôn ngữ
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session[CommonConstants.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext) //đè phương thức 
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION]; //kiểm tra biến session key lưu ở trong LoginController Session là gì bên ngoài cũng gán nhứ thế
            if (session == null) //null thì relect nó về login
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message; //dùng đối tượng tempdata lấy được data trực tiếp từ server về - Tên của lớpTempData, tên của keyAlertMessage
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success"; 
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
   
}