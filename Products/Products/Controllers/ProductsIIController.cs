using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    public class ProductsIIController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductsIIController(ApplicationDbContext db)
        {
            _db = db;
        }

        //// ////

        public IActionResult Index()
        {
            return View(_db.Products.Include(p => p.Category).Where(p => !p.IsDelete).ToList());
            //return View(_db.Products.Where(p => !p.IsDelete).Include(p => p.Category).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories.Where(c => !c.IsDelete), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_db.Categories.Where(c => !c.IsDelete), "Id", "Name");
            //ViewBag.Categories = new SelectList(_db.Categories.Where(c => !c.IsDelete), "Id", "Name", id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            product.UpdatedAt = DateTime.Now;
            _db.Products.Update(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);

            if(product == null)
                return NotFound();

            product.IsDelete = true;
            _db.Products.Update(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}