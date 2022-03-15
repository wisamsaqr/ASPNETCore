using IRPALProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class ArticleController : CMSBaseController
    {
        public ActionResult Index(string q = "", int PageId = 1)
        {
            PageId--;
            string ActionURL = "/CMS/Article/Index?q=" + q;
            int PageRowsCount = 10;

            int Skip = PageId * PageRowsCount;
            var articles = Db.Articles.Where(a => a.IsDelete == false && a.Title.Contains(q)).
                OrderBy(a => a.Id).Skip(Skip).Take(PageRowsCount);

            int RowsCount = Db.Articles.Count(a => a.IsDelete == false && a.Title.Contains(q));

            Paging(ActionURL, PageId, RowsCount, PageRowsCount, 5);

            return View(articles);
        }


        public ActionResult Add()
        {
            ViewBag.ArticleCategories = new SelectList(Db.ArticleCategories, "Id", "Title");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Article article, HttpPostedFileBase Img)
        {
            if(ModelState.IsValid)
            {
                if (Img != null)
                {
                    if (Img.ContentType.Contains("image"))
                    {
                        article.Image = Guid.NewGuid() + System.IO.Path.GetExtension(Img.FileName);
                        Img.SaveAs(Server.MapPath("/Content/Images/Original/") + article.Image);
                        ResizeImage(article.Image);
                    }
                    else
                    {
                        if (Request.IsAjaxRequest())
                            return Json(new { status = 0, msg = "يجب اختيار صورة صحيحة" });
                        else
                        {
                            TempData["msg"] = "d:يجب اختيار صورة صحيحة";
                            return Add();
                        }
                    }
                }
                article.IsDelete = false;
                article.InsertingAdminId = AdminId;
                article.InsertedAt = DateTime.Now;
                Db.Articles.Add(article);
                Db.SaveChanges();

                if (Request.IsAjaxRequest())
                    return Json(new { status = 1, msg = "تمت عملية الإضافة بنجاح" });
                else
                    TempData["msg"] = "s:تمت عملية الإضافة بنجاح";
            }

            if (Request.IsAjaxRequest())
                return Json(new { status = 0, msg = "خطأ في الإدخال" });
            else
                return Add();   //this statement is used becaus it would use ModelState data and fills the elements of the view
        }


        public ActionResult Edit(int id)
        {
            var article = Db.Articles.Find(id);
            if (article == null)
            {
                TempData["msg"] = "d:الرجاء التأكد من رابط العنصر المراد";
                return RedirectToAction("Index");
            }

            ViewBag.ArticleCategories = new SelectList(Db.ArticleCategories, "Id", "Title", article.Id);
            return View(article);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article, HttpPostedFileBase Img)
        {
            if (Db.Articles.Any
            (
                a => a.IsDelete == false &&
                a.Id != article.Id &&
                a.Slug == article.Slug
            ))
            {
                TempData["msg"] = "d:رابط عنوان المقال موجود مسبقًا";
                //return Edit(article.Id);  //calling the Edit(int) is correct, since it creates the dropDownList, but it do much work which isn't required. So simply we can create the dropDownList ourselves, where in either cases the executed code creates it.

                ViewBag.ArticleCategories = new SelectList(Db.ArticleCategories, "Id", "Title", article.Id);
                return View();
            }

            if(ModelState.IsValid)
            {
                if (Img != null)
                {
                    if (Img.ContentType.Contains("image"))
                    {
                        article.Image = Guid.NewGuid() + System.IO.Path.GetExtension(Img.FileName);
                        Img.SaveAs(Server.MapPath("/Content/Images/Original/") + article.Image);
                        ResizeImage(article.Image);
                    }
                    else
                    {
                        TempData["msg"] = "d:الرجاء اختيار صورة صحيحة";
                        ViewBag.ArticleCategories = new SelectList(Db.ArticleCategories, "Id", "Title", article.Id);
                        return View();
                    }
                }
                var articleInDb = Db.Articles.Find(article.Id);
                articleInDb.Title = article.Title;
                articleInDb.Slug = article.Slug;
                articleInDb.CategoryId = article.CategoryId;
                articleInDb.Summary = article.Summary;
                articleInDb.Details = article.Details;
                if (!string.IsNullOrEmpty(article.Image))
                    articleInDb.Image = article.Image;
                articleInDb.Published = article.Published;
                articleInDb.UpdatedAt = DateTime.Now;
                articleInDb.UpdatingAdminId = AdminId;
                Db.Entry(articleInDb).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم تعديل البيانات بنجاح";
                return RedirectToAction("Index");
            }

            ViewBag.ArticleCategories = new SelectList(Db.ArticleCategories, "Id", "Title", article.Id);
            return View();
        }


        public ActionResult Delete(int id)
        {
            var article = Db.Articles.Find(id);
            if (article != null)
            {
                article.IsDelete = true;
                article.UpdatedAt = DateTime.Now;
                article.UpdatingAdminId = AdminId;
                Db.Entry(article).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = App_GlobalResources.Lang.DeleteSucceedMsg;
            }
            else
                TempData["msg"] = "d:المقال غير موجود";

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());

            return RedirectToAction("Index");
        }
    }
}