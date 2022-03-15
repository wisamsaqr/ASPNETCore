using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //// ////

        public IActionResult Index()
        {
            //var categories = _db.Categories;
            return View(_db.Categories.Where(c => !c.IsDelete));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound("No category was found with this id..");
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            category.UpdatedAt = DateTime.Now;
            _db.Categories.Update(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            //if (!_db.Categories.Any(c => c.Id == id))
            //{
            //    return NotFound("ZZZZ: No category was found with this id..");
            //}

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound("No category was found with this id..");
            }

            category.IsDelete = true;
            _db.Categories.Update(category);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}