using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.ActionFilters
{
    //If we want to use this filter as attribute, it must derive FilterAttribute
    public class MyLogAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Diagnostics.Debug.WriteLine("@#$::Log: "
                + filterContext.RouteData.Values["Controller"]
                + " | "
                + filterContext.RouteData.Values["Action"]);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}