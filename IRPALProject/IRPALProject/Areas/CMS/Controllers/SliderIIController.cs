using IRPALProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class SliderIIController : CMSBaseController
    {
        public ActionResult Index(string q = "", int PageId = 1)
        {
            PageId--;
            string ActionURL = "/CMS/SliderII/Index?q=" + q;
            int PageRowsCount = 10;
            var Skip = PageId * PageRowsCount;

            var Sliders = Db.Sliders.Where(s => s.IsDelete == false && s.Title.Contains(q))
                .OrderBy(s => s.Id).Skip(Skip).Take(PageRowsCount);

            Paging(ActionURL, PageId, Db.Sliders.Count(s => s.IsDelete == false && s.Title.Contains(q)),PageRowsCount, 5);

            return View(Sliders);
        }


        public ActionResult SliderManaging(int? id)
        {
            if (id == null)
                return View();
            else
            {
                var slider = Db.Sliders.Find(id);
                if (slider != null)
                    return View(slider);
                else
                    TempData["msg"] = "w:البيانات غير موجودة";
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        //public ActionResult SliderManaging([Bind(Exclude = "Id")Slider slider)    //no bind for Id, Add operation would work, but Edit no because Id would be assigned the default value
        public ActionResult SliderManaging(Slider slider)
        {
            if (slider.Id == 0) //Add
            {
                if (Db.Sliders.Any(s => s.IsDelete == false && s.Title == slider.Title))
                    TempData["msg"] = "d:الشريحة موجودة مسبقًا";
                else if (ModelState.IsValid)
                {
                    slider.IsDelete = false;
                    slider.InsertedAt = DateTime.Now;
                    slider.InsertingAdminId = AdminId;
                    Db.Sliders.Add(slider);
                    Db.SaveChanges();

                    TempData["msg"] = "s:تم إضافة الشريحة";
                    return RedirectToAction("SliderManaging");
                }
            }
            else   //Edit
            {
                if (Db.Sliders.Any(s =>
                s.IsDelete == false
                && s.Id != slider.Id
                && s.Title == slider.Title
            ))
                    TempData["msg"] = "d:البيانات موجودة مسبقًا";
                else if (ModelState.IsValid)
                {
                    Slider sliderInDb = Db.Sliders.Find(slider.Id);
                    sliderInDb.Title = slider.Title;
                    sliderInDb.URL = slider.URL;
                    sliderInDb.Active = slider.Active;
                    sliderInDb.NewWindow = slider.NewWindow;
                    sliderInDb.UpdatedAt = DateTime.Now;
                    sliderInDb.UpdatingAdminId = AdminId;

                    Db.Entry(sliderInDb).State = EntityState.Modified;
                    Db.SaveChanges();

                    TempData["msg"] = "s:تم حفظ البيانات بنجاح";
                    return RedirectToAction("SliderManaging");
                }
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