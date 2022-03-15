using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCIntro.Models;
using System.Data.Entity;

namespace MVCIntro.Controllers
{
    public class CountriesController : Controller
    {
        private IRPALG1Entities Db = new IRPALG1Entities();
        public ActionResult Index()
        {
            var Countries = Db.Countries.ToList();
            return View(Countries);
        }

        public ActionResult Details(int id)
        {
            var country = Db.Countries.SingleOrDefault(c => c.Id == id);
            return View(country);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            if(ModelState.IsValid)
            {
                Db.Countries.Add(country);
                Db.SaveChanges();

                TempData["msg"] = "i:Country is added successfully";
                TempData["msg2"] = "info:Country is added successfully";

                return RedirectToAction("Create");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            Country country = Db.Countries.Find(id);

            return View(country);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            if(ModelState.IsValid)
            {
                var countryInDb = Db.Countries.Find(country.Id);
                countryInDb.Name= country.Name;
                countryInDb.Iso2 = country.Iso2;
                countryInDb.Code = country.Code;
                Db.SaveChanges();

                TempData["msg"] = "i:Country has been updated successfully";
                TempData["msg2"] = "info:Country has been updated successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            var country = Db.Countries.Find(id);
            Db.Countries.Remove(country);
            Db.SaveChanges();

            TempData["msg"] = "i:Country has been deleted successfully";
            TempData["msg2"] = "info:Country has been deleted successfully";

            return RedirectToAction("Index");
        }
    }
}