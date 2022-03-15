using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.Areas.WithCookie.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (Request.Cookies["UserId"] == null)
            {
                TempData["msg"] = "d:Please login to get access to the page";

                filterContext.Result = new RedirectResult("/Logins/WithCookieLogin");
            }
        }
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult WithCookieLogout()
        {
            Response.Cookies["UserId"].Expires = DateTime.Now;    //this finishes the cookie time which make it get removed from client, since it is expiered
            Response.Cookies["FullName"].Expires = DateTime.Now;    //this finishes the cookie time which make it get removed from client, since it is expiered

            return Redirect("/Logins/WithCookieLogin");
        }
    }
}