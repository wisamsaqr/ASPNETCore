using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCIntro.Models;
using MVCIntro.ViewModels;

namespace MVCIntro.Controllers
{
    public class LoginsController : Controller
    {
        private IRPALG1Entities db = new IRPALG1Entities();



        public ActionResult WithoutSessionCookieLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WithoutSessionCookieLogin(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);

                if (user == null)
                {
                    //TempData["msg"] = "d:Invalid username or password";
                    TempData["msg2"] = "danger:Invalid username or password";
                }
                else
                {
                    return Redirect("/WithoutSessionCookie");
                }
            }
            return View();
        }





        //////////////////////////////////////////////////////////////////





        public ActionResult WithSessionLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WithSessionLogin(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);

                if (user == null)
                {
                    //TempData["msg"] = "d:Invalid username or password";
                    TempData["msg2"] = "danger:Invalid username or password";
                }
                else
                {
                    Session["UserId"] = user.Id;
                    Session["FullName"] = user.FullName;
                    return Redirect("/WithSession");
                }
            }
            return View();
        }





        //////////////////////////////////////////////////////////////////





        public ActionResult WithCookieLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WithCookieLogin(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);

                if (user == null)
                {
                    //TempData["msg"] = "d:Invalid username or password";
                    TempData["msg2"] = "danger:Invalid username or password";
                }
                else
                {
                    Response.Cookies.Add(new HttpCookie("UserId", user.Id.ToString()));
                    Response.Cookies.Add(new HttpCookie("FullName", user.FullName));

                    Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(14);
                    Response.Cookies["FullName"].Expires = DateTime.Now.AddDays(14);

                    return Redirect("/WithCookie");
                }
            }
            return View();
        }





        //////////////////////////////////////////////////////////////////





        public ActionResult FormsLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormsLogin(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);
                if (user != null)
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.FullName, login.RememberMe);

                    return RedirectToAction("Index", "Home", new { area = "WithForms" });
                }
                else
                    TempData["msg"] = "d:Invalid username or password";
            }

            return View();
        }








        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}