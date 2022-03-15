using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.Areas.WithoutSessionCookie.Controllers
{
    public class HomeController : Controller
    {
        // GET: WithoutSessionCookie/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}