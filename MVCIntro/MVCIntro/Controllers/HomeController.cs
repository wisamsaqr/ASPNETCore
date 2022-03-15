using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCIntro.Models;

namespace MVCIntro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult Lang(string id)
        {
            string Lang = id;

            if (Lang.ToLower() == "en" || Lang.ToLower() == "ar")
            {
                Response.Cookies.Add(new HttpCookie("lang", Lang));
                Response.Cookies["lang"].Expires = DateTime.Now.AddDays(15);
            }

            return RedirectToAction("Register");
        }

        public ActionResult Register()
        {
            string UserDefaultLang;
            if (Request.Cookies["lang"] != null)
                UserDefaultLang = Request.Cookies["lang"].Value;
            else
                UserDefaultLang = Request.UserLanguages[0];

            string UserCulture = UserDefaultLang.Contains("ar") ? "ar-eg" : "en-gb";


            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(UserCulture);

            ViewBag.IsRtl = UserDefaultLang.Contains("ar");

            return View();
        }


        #region DropDownList Testing
        public ActionResult DropDownListTest()
        {
            List<Student> StudentsList = new List<Student>
            {
                new Student {Id=11, Name="AAA" },
                new Student {Id=22, Name="BBB" },
                new Student {Id=33, Name="CCC" }
            };
            ViewBag.StdsDS = new SelectList(StudentsList, "Id", "Name");

            IEnumerable<Department> DepartmentsList = new List<Department>
            {
                new Department { DepId = 111, Name = "XXX" },
                new Department { DepId = 222, Name = "YYY" },
                new Department { DepId = 333, Name = "ZZZ" }
            };
            ViewBag.DepId = new SelectList(DepartmentsList, "DepId", "Name");

            return View();
        }
        #endregion DropDownList Testing



        #region View Generated Links For BeginForm & ActionLink
        public ActionResult Action()
        {
            return View();
            //return View("ActionII");
            //return RedirectToAction("ActionII");
        }
        public ActionResult ActionII()
        {
            return View();
        }
        #endregion View Generated Links For BeginForm & ActionLink
    }

    #region Classes for DropDownList Testing
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Department
    {
        public int DepId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
    #endregion DropDownListTest



    public class InView
    {
        public int X = 5;
    }
}