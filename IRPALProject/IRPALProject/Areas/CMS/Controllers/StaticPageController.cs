using IRPALProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class StaticPageController : CMSBaseController
    {
        public ActionResult Index(string q = "", int PageId = 1)
        {
            PageId--;
            string ActionURL = "/CMS/StaticPage/Index?q=" + q;
            int PageRowsCount = 10;
            var Skip = PageId * PageRowsCount;

            var StaticPages = Db.StaticPages.Where(s => s.IsDelete == false && s.Title.Contains(q))
                .OrderBy(s => s.Id).Skip(Skip).Take(PageRowsCount);

            Paging(ActionURL, PageId, Db.StaticPages.Count(s => s.IsDelete == false && s.Title.Contains(q)),PageRowsCount, 5);

            return View(StaticPages);
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(StaticPage staticPage, HttpPostedFileBase Img)
        {
            if (Db.StaticPages.Any(s => s.IsDelete == false && s.Title == staticPage.Title))
                TempData["msg"] = "d:البيانات موجودة مسبقًا";
            else if (ModelState.IsValid)
            {
                if (Img != null)
                {
                    if (Img.ContentType.Contains("image"))
                    {
                        staticPage.Image = Guid.NewGuid() + System.IO.Path.GetExtension(Img.FileName);
                        Img.SaveAs(Server.MapPath("/Content/Images/Original/") + staticPage.Image);
                        ResizeImage(staticPage.Image);
                    }
                    else
                    {
                        TempData["msg"] = "d:يجب اختيار صورة صحيحة";
                        return View();
                    }
                }
                staticPage.IsDelete = false;
                staticPage.InsertedAt = DateTime.Now;
                staticPage.InsertingAdminId = AdminId;
                Db.StaticPages.Add(staticPage);
                Db.SaveChanges();

                TempData["msg"] = "s:تم إضافة البيانات";
                return RedirectToAction("Add");
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            var staticPage = Db.StaticPages.Find(id);
            if (staticPage != null)
                return View(staticPage);
            else
                TempData["msg"] = "w:البيانات غير موجودة";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(StaticPage staticPage, HttpPostedFileBase Img)
        {
            if (Db.StaticPages.Any(s =>
                s.IsDelete == false
                && s.Id != staticPage.Id 
                && s.Title == staticPage.Title
            ))
                TempData["msg"] = "d:البيانات موجودة مسبقًا";
            else if (ModelState.IsValid)
            {
                if (Img != null)
                {
                    if (Img.ContentType.Contains("image"))
                    {
                        staticPage.Image = Guid.NewGuid() + System.IO.Path.GetExtension(Img.FileName);
                        Img.SaveAs(Server.MapPath("/Content/Images/Original/") + staticPage.Image);
                        ResizeImage(staticPage.Image);
                    }
                    else
                    {
                        TempData["msg"] = "d:الرجاء اختيار صورة صحيحة";
                        return View();
                    }
                }

                StaticPage staticPageInDb = Db.StaticPages.Find(staticPage.Id);
                staticPageInDb.Title = staticPage.Title;
                staticPageInDb.Slug = staticPage.Slug;
                staticPageInDb.Details = staticPage.Details;
                staticPageInDb.Image = !string.IsNullOrEmpty(staticPage.Image) ? staticPage.Image : staticPageInDb.Image;
                staticPageInDb.Published = staticPage.Published;
                staticPageInDb.IsDelete = false;
                staticPageInDb.UpdatedAt = DateTime.Now;
                staticPageInDb.InsertingAdminId = AdminId;

                Db.Entry(staticPageInDb).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حفظ البيانات بنجاح";
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            var staticPage = Db.StaticPages.Find(id);
            if (staticPage == null)
                TempData["msg"] = "w:البيانات غير موجودة";
            else
            {
                staticPage.IsDelete = true;
                staticPage.UpdatedAt = DateTime.Now;
                staticPage.UpdatingAdminId = AdminId;

                Db.Entry(staticPage).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حذف البيانات بنجاح";
            }

                return RedirectToAction("Index");
        }
    }
}