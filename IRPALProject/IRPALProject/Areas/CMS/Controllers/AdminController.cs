using IRPALProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace IRPALProject.Areas.CMS.Controllers
{
    public class AdminController : CMSBaseController
    {
        public ActionResult Index(string q = "", int PageId = 1)
        {
            PageId--;
            string ActionURL = "~/CMS/Admin?q=" + q;
            int PageRowsCount = 10;
            int Skip = PageId * PageRowsCount;

            var admins = Db.Admins.Where(a => a.IsDelete == false && a.FullName.Contains(q))
                .OrderBy(a => a.Id).Skip(Skip).Take(PageRowsCount);

            Paging(ActionURL, PageId, Db.Admins.Count(a => a.IsDelete == false && a.FullName.Contains(q)), PageRowsCount, 5);

            return View(admins);
        }


        public ActionResult Add()
        {
            return View();
        }



        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Admin admin, string Password)
        {
            if (Db.Admins.Any(a => a.IsDelete == false && a.Email == admin.Email))
                TempData["msg"] = "d:الإيميل موجود مسبقًا";
            else if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = admin.Email, Email = admin.Email };
                var result = await UserManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    admin.AspNetUserId = user.Id;
                    admin.IsDelete = false;
                    admin.InsertedAt = DateTime.Now;
                    admin.InsertingAdminId = AdminId;
                    Db.Admins.Add(admin);
                    Db.SaveChanges();

                    TempData["msg"] = "s:تم إضافة المستخدم بنجاح";
                    return RedirectToAction("Add");
                }
                else
                {
                    string errors = "d:";
                    foreach (var error in result.Errors)
                        errors += error + ", ";
                    errors = errors.Substring(0, errors.Length - 2);
                    TempData["msg"] = errors;
                }
            }
            return View();
        }



        public ActionResult Edit(int id)
        {
            var admin = Db.Admins.Find(id);
            if (admin != null)
            {
                return View(admin);
            }

            TempData["msg"] = "d:البيانات غير موجودة";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin)
        {
            //if (Db.Admins.Any(a => a.IsDelete == false && a.Id != admin.Id && a.Email == admin.Email))
            //    TempData["msg"] = "d:الإيميل موجود مسبقًا";


            if (admin.Email != Db.Admins.Find(admin.Id).Email)
                TempData["msg"] = "d:لا يمكن التعديل على الإيميل";
            else if (ModelState.IsValid)
            {
                var adminInDb = Db.Admins.Find(admin.Id);
                adminInDb.FullName = admin.FullName;
                adminInDb.Mobile = admin.Mobile;
                adminInDb.Active = admin.Active;

                adminInDb.UpdatedAt = DateTime.Now;
                adminInDb.UpdatingAdminId = AdminId;
                Db.Entry(adminInDb).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حفظ البيانات بنجاح";
                return RedirectToAction("Index");
            }
            return View();
        }



        public ActionResult Delete(int id)
        {
            var admin = Db.Admins.Find(id);
            if (admin != null)
            {
                admin.IsDelete = true;
                admin.UpdatedAt = DateTime.Now;
                admin.UpdatingAdminId = AdminId;
                Db.Entry(admin).State = EntityState.Modified;
                Db.SaveChanges();

                TempData["msg"] = "s:تم حذف المستخدم بنجاح";
            }
            else
                TempData["msg"] = "s:المستخدم غير موجود";

            return RedirectToAction("Index");
        }


        public ActionResult Permission(int id)
        {
            var admin = Db.Admins.Find(id);
            if (admin != null)
            {
                return View(admin);
            }
            else
            {
                TempData["msg"] = "d:البيانات غير موجودة";
                return RedirectToAction("Index");
            }
        }

        [ValidateAntiForgeryToken, HttpPost]
        //public ActionResult Permission(int Id, int[] LinkIds)   //Trainer
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var admin = Db.Admins.Find(Id);
        //        //foreach (var link in admin.Links) //this statement doesn't work
        //        foreach (var link in admin.Links.ToList())
        //        {
        //            admin.Links.Remove(link);
        //        }
        //        Db.SaveChanges();

        //        foreach (int linkId in LinkIds)
        //        {
        //            var link = Db.Links.Find(linkId);
        //            admin.Links.Add(link);
        //        }
        //        Db.SaveChanges();

        //        TempData["msg"] = "s:تمت حفظ البيانات بنجاح";
        //        return Redirect("/CMS/Admin/Permission/" + Id);
        //    }
        //    return Permission(Id);
        //}


        public ActionResult Permission(int Id, int[] LinkIds)   //Me
        {
            if (ModelState.IsValid)
            {
                var admin = Db.Admins.Find(Id);

                admin.Links.Clear();
                Db.SaveChanges();

                var adminLinks = Db.Links.Where(l => LinkIds.Contains(l.Id));
                foreach (var link in adminLinks)
                    admin.Links.Add(link);
                Db.SaveChanges();

                TempData["msg"] = "s:تمت حفظ البيانات بنجاح";
                return Redirect("/CMS/Admin/Permission/" + Id);
            }
            return Permission(Id);
        }







        //For Helper Methods Experiments
        public ActionResult HelperExperiments()
        {
            IRPALProject.Models.Admin ad = new IRPALProject.Models.Admin { InsertedAt = DateTime.Now };
            //IRPALProject.Models.Admin ad2 = null;

            return View(ad);
        }
        public ActionResult HelperExperiments2()
        {
            List<IRPALProject.Models.Admin> adsList = new List<IRPALProject.Models.Admin>
            {
                new IRPALProject.Models.Admin { FullName = "AAA" },
                new IRPALProject.Models.Admin { FullName = "BBB" },
                new IRPALProject.Models.Admin { FullName = "CCC" }
            };

            return View(adsList);
        }
    }
}