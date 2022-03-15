using IRPALProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class SliderController : CMSBaseController
    {
        public ActionResult Index(string q = "", int PageId = 1)
        {
            PageId--;
            string ActionURL = "/CMS/Slider/Index?q=" + q;
            int PageRowsCount = 10;
            var Skip = PageId * PageRowsCount;

            var Sliders = Db.Sliders.Where(s => s.IsDelete == false && s.Title.Contains(q))
                .OrderBy(s => s.Id).Skip(Skip).Take(PageRowsCount);

            Paging(ActionURL, PageId, Db.Sliders.Count(s => s.IsDelete == false && s.Title.Contains(q)),PageRowsCount, 5);

            return View(Sliders);
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(Slider slider, HttpPostedFileBase Img)
        {
            if (Db.Sliders.Any(s => s.IsDelete == false && s.Title == slider.Title))
                TempData["msg"] = "d:الشريحة موجودة مسبقًا";
            else if (ModelState.IsValid)
            {
                //The next code forces users to upload an image when adding a slider.
                //Only in adding sliders users are forced to add images, adding articles and static pages no problem
                if (Img == null)
                    TempData["msg"] = "d:يجب اختيار صورة";
                else if (!Img.ContentType.Contains("image"))
                    TempData["msg"] = "d:يجب اختيار صورة صحيحة";
                else
                {
                    slider.Image = Guid.NewGuid() + System.IO.Path.GetExtension(Img.FileName);
                    Img.SaveAs(Server.MapPath("/Content/Images/Original/") + slider.Image);
                    ResizeImage(slider.Image);

                    slider.IsDelete = false;
                    slider.InsertedAt = DateTime.Now;
                    slider.InsertingAdminId = AdminId;
                    Db.Sliders.Add(slider);
                    Db.SaveChanges();

                    TempData["msg"] = "s:تم إضافة الشريحة";
                    return RedirectToAction("Add");
                }
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            var slider = Db.Sliders.Find(id);
            if (slider != null)
                return View(slider);
            else
                TempData["msg"] = "w:البيانات غير موجودة";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider, HttpPostedFileBase Img)
        {
            if (Db.Sliders.Any(s =>
                s.IsDelete == false
                && s.Title == slider.Title
                && s.Id != slider.Id 
            ))
                TempData["msg"] = "d:البيانات موجودة مسبقًا";
            else if (ModelState.IsValid)
            {
                Slider sliderInDb = Db.Sliders.Find(slider.Id);

                if (Img != null)
                {
                    if (Img.ContentType.Contains("image"))
                    {
                        slider.Image = Guid.NewGuid() + System.IO.Path.GetExtension(Img.FileName);
                        Img.SaveAs(Server.MapPath("/Content/Images/Original/") + slider.Image);
                        ResizeImage(slider.Image);

                        sliderInDb.Image = slider.Image;
                    }
                    else
                    {
                        TempData["msg"] = "d:الرجاء اختيار صورة صحيحة";
                        return View();
                    }
                }

                sliderInDb.Title = slider.Title;
                sliderInDb.URL = slider.URL;
                sliderInDb.Active = slider.Active;
                sliderInDb.NewWindow = slider.NewWindow;
                sliderInDb.UpdatedAt = DateTime.Now;
                sliderInDb.UpdatingAdminId = AdminId;

                Db.Entry(sliderInDb).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حفظ البيانات بنجاح";
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            var slider = Db.Sliders.Find(id);
            if (slider == null)
                TempData["msg"] = "w:البيانات غير موجودة";
            else
            {
                slider.IsDelete = true;
                slider.UpdatedAt = DateTime.Now;
                slider.UpdatingAdminId = AdminId;

                Db.Entry(slider).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حذف البيانات بنجاح";
            }

                return RedirectToAction("Index");
        }
    }
}