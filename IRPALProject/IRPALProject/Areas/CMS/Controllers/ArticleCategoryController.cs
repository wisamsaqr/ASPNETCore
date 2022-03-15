using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IRPALProject.Models;
using System.Net;
using System.Data.Entity;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class ArticleCategoryController : CMSBaseController
    {
        public ActionResult Index(string q = "", int PageId = 1)
        {
            PageId--;
            string ActionURL = "/CMS/ArticleCategory/Index?q=" + q;
            var PageRowsCount = 10;
            var Skip = PageId * PageRowsCount;

            var ArticleCategories = Db.ArticleCategories
                .Where
                (
                    ac => ac.IsDelete == false &&
                    ac.Title.Contains(q)
                )
                .OrderByDescending(ac => ac.Id)
                .Skip(Skip)
                .Take(PageRowsCount);

            int PagesBeforeOrAfterTheCurrentPage = 5;
            Paging
            (
                ActionURL,
                PageId,
                Db.ArticleCategories.Count(ac => ac.IsDelete == false && ac.Title.Contains(q)),
                PageRowsCount,
                PagesBeforeOrAfterTheCurrentPage
            );

            return View(ArticleCategories.ToList());
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ArticleCategory articleCategory)
        {
            #region Prevent Title Duplication
            //we can use database unique constraint
            if (ModelState.IsValid)
            {
                //var acExists = Db.ArticleCategories.Count
                //    (
                //        ac => ac.IsDelete == false &&
                //        ac.Title == articleCategory.Title
                //    ) > 0;

                var acExists = Db.ArticleCategories.Any
                    (
                        ac => ac.IsDelete == false &&
                        ac.Title == articleCategory.Title
                    );
                if (acExists)
                {
                    TempData["msg"] = "d:تصنيف المقال موجود مسبقًا";
                    return View();
                }
                #endregion Prevent Title Duplication

                articleCategory.InsertingAdminId = AdminId;
                articleCategory.InsertedAt = DateTime.Now;      //in database by default constraint
                articleCategory.IsDelete = false;               //in database by default constraint
                Db.ArticleCategories.Add(articleCategory);
                Db.SaveChanges();

                TempData["msg"] = "s:تمت عملية الإضافة بنجاح";
                return RedirectToAction("Add");
                //return View();  //this would use ModelState to populate the html elements, and, as obvious, the ModelState is full of data
            }

            return View();
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["msg"] = "d:الرجاء التأكد من صحة الرابط";
                return RedirectToAction("Index");

                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var articleCategory = Db.ArticleCategories.Find(id);
            if (articleCategory == null)
            {
                TempData["msg"] = "d:الرجاء التأكد من الرابط";
                return RedirectToAction("Index");

                //return new HttpNotFoundResult();
            }

            return View(articleCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleCategory articleCategory)
        {
            var acExists = Db.ArticleCategories.Any
            (
                ac => ac.IsDelete == false &&
                ac.Title == articleCategory.Title &&
                ac.Id != articleCategory.Id
            );

            if (acExists)
            {
                TempData["msg"] = "d:تصنيف المقال موجود مسبقًا";
                return View();
            }

            if (ModelState.IsValid)
            {
                var articleCategoryInDb = Db.ArticleCategories.Find(articleCategory.Id);
                articleCategoryInDb.Title = articleCategory.Title;
                articleCategoryInDb.UpdatedAt = DateTime.Now;
                articleCategoryInDb.UpdatingAdminId = AdminId;
                Db.Entry(articleCategoryInDb).State = EntityState.Modified; //this line of code is not required, but trainer basil used it
                Db.SaveChanges();

                TempData["msg"] = "تم تعديل البيانات بنجاح";

                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Delete(int id)
        {
            var articleCategoryInDb = Db.ArticleCategories.Find(id);

            if (articleCategoryInDb == null)
                TempData["msg"] = "d:تصنيف المقال غير موجود";
            else
            {
                //Db.ArticleCategories.Remove(articleCategoryInDb);     //True Delete

                articleCategoryInDb.IsDelete = true;                    //Soft Delete
                articleCategoryInDb.UpdatedAt = DateTime.Now;
                articleCategoryInDb.UpdatingAdminId = AdminId;
                Db.Entry(articleCategoryInDb).State = EntityState.Modified;
                Db.SaveChanges();
                TempData["msg"] = "s:تمت عملية الحذف بنجاح";
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());

            return RedirectToAction("Index");
        }
    }
}