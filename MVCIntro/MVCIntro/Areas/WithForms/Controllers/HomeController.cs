using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.Areas.WithForms.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult FormsLogout()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            return RedirectToAction("FormsLogin", "Logins", new { area = "" });
        }
    }
}