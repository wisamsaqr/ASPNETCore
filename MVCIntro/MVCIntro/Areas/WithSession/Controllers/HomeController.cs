using MVCIntro.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCIntro.Areas.WithSession.Controllers
{
    public class HomeController : Controller
    {
        #region Checking Session directly inside the Action
        //public ActionResult Index()
        //{
        //    if (Session["UserId"] == null)
        //    {
        //        TempData["msg"] = "d:Please login to get access to the page";

        //        return Redirect("/Logins/WithSessionLogin");
        //        //return RedirectToAction("WithSessionLogin", "Logins", new { area = "" });
        //    }
        //    return View();
        //}
        #endregion Checking Session directly inside the Action



        //////////////////////////////////////////////////////////////////////////////////////



        #region Checking Session Using SessionAuthorize Filter
        //[SessionAuthorize]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        #endregion Checking Session Using SessionAuthorize Filter



        /////////////////////////////////////////////////////////////////////////////////////



        #region Checking Session Using OnActionExecuting
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //if (filterContext.HttpContext.Session["UserId"] == null)
            if (Session["UserId"] == null)
            {
                TempData["msg"] = "d:Please login to get access to the page";
                filterContext.Result = new RedirectResult("/Logins/WithSessionLogin");
                //Response.Redirect("/Logins/WithSessionLogin");    //this statement doesn't accept TempData
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        #endregion Checking Session Using OnActionExecuting



        //////////////////////////////////////////////////////////////////////////////////////



        #region Checking Session Using Initialize Method
        //Not good solution, since TempData doesn't work with Response.Redirect().
        //In all cases, It is very recommended to not use Initialize method. OnActionExecuting() must be used instead.

        //    protected override void Initialize(RequestContext requestContext)
        //    {
        //        base.Initialize(requestContext);
        //        if (Session["UserId"] == null)
        //        {
        //            TempData["msg"] = "d:Please login to get access to the page";
        //            Response.Redirect("/Logins/WithSessionLogin");    //this statement doesn't accept TempData
        //        }
        //    }
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }
        #endregion Checking Session Using Initialize Method



        public ActionResult WithSessionLogout()
        {
            Session.Clear();
            return RedirectToAction("WithSessionLogin", "Logins", new { area = "" });
        }
    }
}