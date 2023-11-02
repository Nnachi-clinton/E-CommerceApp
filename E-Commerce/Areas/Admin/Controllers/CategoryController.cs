using E_commerce.Data.Repository.IRepository;
using E_commerce.Utility;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;

        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var getAll = _db.Category.GetAll().ToList();
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
                _db.Category.Add(category);
                _db.Save();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _db.Category.Get(c => c.Id == Id);
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
                _db.Category.Update(category);
                _db.Save();
                TempData["Success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _db.Category.Get(c => c.Id == Id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var category = _db.Category.Get(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Category.Remove(category);
            _db.Save();
            TempData["Success"] = "Category Deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
