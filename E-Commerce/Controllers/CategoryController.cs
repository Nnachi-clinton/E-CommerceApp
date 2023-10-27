using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var getAll = _db.Categories.ToList();
            return View(getAll);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "name and displayorder cannot match.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
           
        } 
        public IActionResult Edit(int Id)
        {
            if (Id==null || Id ==0)
            {
                return NotFound();
            }
            var category = _db.Categories.FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
           
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();
           
        }
        public IActionResult Delete(int Id)
        {
            if (Id==null || Id ==0)
            {
                return NotFound();
            }
            var category = _db.Categories.FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var category = _db.Categories.FirstOrDefault(x=>x.Id ==Id);
            if (category == null)
            {
                return NotFound();
            }           
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["Success"] = "Category Deleted successfully";
            return RedirectToAction("Index");   
           
        }
    }
}
