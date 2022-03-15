using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.Controllers
{
    public class AjaxExamplesController : Controller
    {
        public ActionResult LoadTest()
        {
            return View();
        }

        public ActionResult GetTest()
        {
            return View();
        }

        public ActionResult PostTest()
        {
            return View();
        }

        public ActionResult AjaxTest()
        {
            return View();
        }







        //This action returns view (html string)
        public ActionResult Page()
        {
            return View();
        }


        //This action receives request by Post method and just returns some content (general string)
        [HttpPost]
        public ActionResult PostRequestContent(string age, string gender)
        {
            System.Threading.Thread.Sleep(2500);
            return Content("Post" + DateTime.Now + "<br />" + "Gender: " + gender + ", Age: " + age);
        }


        //This action receives request by Get method and just returns some content (general string)
        [HttpGet]
        public ActionResult GetRequestContent(string age, string gender)
        {
            System.Threading.Thread.Sleep(2500);
            return Content("Get" + DateTime.Now + "<br />" + "Gender: <b>" + gender + "</b>, Age: <b>" + age + "</b>");
        }


        //This action returns data of xml type (xml string)
        public ActionResult XmlAction()
        {
            Response.ContentType = "text/xml";
            return View();
        }
        public ActionResult JsonAction()
        {
            return Json(new[]
            { 
                new
                {
                    Name = "Mohammed",
                    Age = 55,
                    Edu = "Computer"
                },
                new
                {
                    Name = "Samera",
                    Age = 20,
                    Edu = "Finance"
                }
            },
            JsonRequestBehavior.AllowGet
            );
        }

        public ActionResult JsonActionSingleObject()
        {
            return Json(
                new
                {
                    Name = "Single",
                    Age = 22,
                    Edu = "IT"
                    //,Address = new
                    //{
                    //    Country = "Palestine",
                    //    City = "Rafah"
                    //}
                },
                JsonRequestBehavior.AllowGet
            );
        }
    }
}