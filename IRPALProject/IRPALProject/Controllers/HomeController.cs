using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IRPALProject.Models;



namespace IRPALProject.Controllers
{
    public class HomeController : FrontBaseController
    {
        public ActionResult Index()
        {
            var sliders = Db.Sliders.Where(s => s.IsDelete == false && s.Active).OrderByDescending(s => s.Id).Take(5);
            ViewBag.sliders = sliders;

            ViewBag.top3Articles = Db.Articles.Where(a => a.IsDelete == false && a.Published)
                .OrderByDescending(a => a.Id).Take(3);


            return View();
        }

        public ActionResult Article(string id)
        {
            string slug = id;
            var article = Db.Articles.FirstOrDefault(a => a.IsDelete == false && a.Published && a.Slug == slug);
            if(article == null)
            {
                TempData["msg"] = "d:الرجاء التأكد من البيانات المطلوبة";

                return Redirect("/");
            }

            ViewBag.otherArticles = Db.Articles.Where
            (
                a =>
                a.IsDelete == false &&
                a.Published &&
                a.Id != article.Id &&
                a.CategoryId == article.CategoryId
            )
            .OrderByDescending(a => a.Id).Take(3);

            return View(article);
        }

        public ActionResult Articles(string q = "", int PageId = 1, int? CategoryId = null)
        {
            PageId--;
            //string ActionURL = "/Home/Articles?q=" + q + "&CategoryId=" + CategoryId;
            //string ActionURL = String.Format("/Home/Articles?q={0}&CategoryId={1}", q, CategoryId);
            string ActionURL = $"/Home/Articles?q={q}&CategoryId={CategoryId}";
            int PageRowsCount = 10;

            int Skip = PageId * PageRowsCount;
            var articles = Db.Articles.Where(a => a.IsDelete == false && a.Title.Contains(q) &&
                a.Published && (a.CategoryId == CategoryId || CategoryId == null))
                .OrderBy(a => a.Id).Skip(Skip).Take(PageRowsCount);

            int RowsCount = Db.Articles.Count(a => a.IsDelete == false && a.Title.Contains(q) &&
                a.Published && (a.CategoryId == CategoryId || CategoryId == null));

            ViewBag.CategoryList = new SelectList(Db.ArticleCategories.Where(ac => ac.IsDelete == false),
                "Id", "Title", CategoryId);

            Paging(ActionURL, PageId, RowsCount, PageRowsCount, 5);

            return View(articles);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}