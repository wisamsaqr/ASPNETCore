using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.ActionFilters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session["UserId"] == null)
            {
                filterContext.Controller.TempData["msg"] = "d:Please login to get access to the page";
                filterContext.Result = new RedirectResult("/Logins/WithSessionLogin");

                #region Important to know
                //filterContext.ActionParameters["parameter"] = "value";
                //filterContext.Controller.ViewBag;
                //filterContext.HttpContext.Response;
                //filterContext.Result;
                //filterContext.RouteData;
                //filterContext.RouteData.Values["Controller"];
                //filterContext.RequestContext;
                #endregion Important to know
            }
        }
    }





    //public class SessionAuthorizeAttribute : FilterAttribute, IActionFilter
    //{
    //    void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (filterContext.HttpContext.Session["UserId"] == null)
    //        {
    //            filterContext.Controller.TempData["msg"] = "d:Please login to get access to the page";
    //            filterContext.Result = new RedirectResult("/Logins/WithSessionLogin");
    //        }
    //    }



    //    void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext) { }
    //}
}