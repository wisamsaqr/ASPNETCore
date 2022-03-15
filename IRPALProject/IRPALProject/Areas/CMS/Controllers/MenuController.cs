using IRPALProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class MenuController : CMSBaseController
    {
        public ActionResult Index(int id = 0)
        {
            if (id != 0)
            {
                var menu = Db.Menus.Find(id);
                if (menu == null)
                {
                    TempData["msg"] = "d:الرجاء التأكد من الرابط المطلوب";
                    return RedirectToAction("Index");
                }

                ViewBag.MenuTitle = menu.Title;
            }
            else
                ViewBag.MenuTitle = "الرئيسيسة";

            var menus = Db.Menus.Where(m => m.IsDelete == false && m.ParentId == id);

            ViewBag.Id = id; //ViewBag.ParentId = id; //Trainer
            return View(menus);
        }


        //This action adds a new menu, thus it takes the id of the parent menu into which it adds the menu (the id of its parent)
        //Here we imagin that the Add action belongs to the parent menu into which the new menu is added
        //public ActionResult Add(int id)
        //{
        //    RouteData.Values.Remove("id");

        //    if (id != 0)
        //    {
        //        var menu = Db.Menus.Find(id);
        //        if (menu == null)
        //        {
        //            TempData["msg"] = "d:الرجاء التأكد من الرابط المطلوب";
        //            return RedirectToAction("Index");
        //        }

        //        ViewBag.MenuTitle = menu.Title;
        //    }
        //    else
        //        ViewBag.MenuTitle = "الرئيسيسة";

        //    ViewBag.ParentId = id;

        //    return View();
        //}

        /* Here we imagin that the Add action belongs to the the new menu and it takes its parent id as a ont of its properties
         * to figure out where it gonna be added inside the mune.
         * Either actions works with the same view.
         */
        public ActionResult Add(int id)
        {
            var parentId = id;
            RouteData.Values.Remove("id");

            if (parentId != 0)
            {
                var menu = Db.Menus.Find(parentId);
                if (menu == null)
                {
                    TempData["msg"] = "d:الرجاء التأكد من الرابط المطلوب";
                    return RedirectToAction("Index");
                }

                ViewBag.MenuTitle = menu.Title;
            }
            else
                ViewBag.MenuTitle = "الرئيسيسة";

            ViewBag.ParentId = parentId;

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Menu menu)
        {
            if (Db.Menus.Any(m => m.IsDelete == false && m.Title == menu.Title && m.ParentId == menu.ParentId))
            {
                TempData["msg"] = "d:القائمة موجودة مسبقًا";
                return Add(menu.ParentId.Value);
            }
            if (ModelState.IsValid)
            {
                menu.IsDelete = false;
                menu.InsertedAt = DateTime.Now;
                menu.InsertingAdminId = AdminId;
                Db.Menus.Add(menu);
                Db.SaveChanges();
                TempData["msg"] = "s:تم إضافة القائمة بنجاح";
                return RedirectToAction("Add", new { id = menu.ParentId });
            }
            return Add(menu.ParentId.Value);
        }


        public ActionResult Edit(int id)
        {
            var menu = Db.Menus.Find(id);

            if (menu != null)
                return View(menu);
            else
            {
                TempData["msg"] = "d:الرجاء التأكد من البيانات المطلوبة";
                return RedirectToAction("Index");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (Db.Menus.Any(m =>
                m.IsDelete == false &&
                m.Title == menu.Title &&
                m.Id != menu.Id &&
                m.ParentId == menu.ParentId)
            )
            {
                TempData["msg"] = "d:القائمة موجودة مسبقًا";
                return View();
            }
            else if (ModelState.IsValid)
            {
                var menuInDb = Db.Menus.Find(menu.Id);
                menuInDb.Title = menu.Title;
                menuInDb.URL = menu.URL;
                menuInDb.Active = menu.Active;
                menuInDb.NewWindow = menu.NewWindow;
                menuInDb.UpdatedAt = DateTime.Now;
                menuInDb.UpdatingAdminId = AdminId;
                Db.Entry(menuInDb).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حفظ البيانات بنجاح";
                return RedirectToAction("Index", new { id = menuInDb.ParentId });
            }
            else
                return View();
        }


        public ActionResult Delete(int id)
        {
            var menu = Db.Menus.Find(id);
            if (menu == null)
                TempData["msg"] = "w:البيانات غير موجودة";
            else
            {
                menu.IsDelete = true;
                menu.UpdatedAt = DateTime.Now;
                menu.UpdatingAdminId = AdminId;

                Db.Entry(menu).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حذف البيانات بنجاح";
            }

            return RedirectToAction("Index", new { id = menu.ParentId });
        }


        public ActionResult Active(int? id)
        {
            if (id == null)
                return Json(new { Msg = "Invalid Request" }, JsonRequestBehavior.AllowGet);
                //return Content("Invalid Request");

            var menuInDb = Db.Menus.Find(id);
            if (menuInDb == null)
                return Json(new { Msg = "Invalid Id"}, JsonRequestBehavior.AllowGet);

            menuInDb.Active = !menuInDb.Active;

            menuInDb.UpdatedAt = DateTime.Now;
            menuInDb.UpdatingAdminId = AdminId;

            Db.Entry(menuInDb).State = EntityState.Modified;
            Db.SaveChanges();

            return Json(new { Msg = "تم التحديث"}, JsonRequestBehavior.AllowGet);
        }
    }
}