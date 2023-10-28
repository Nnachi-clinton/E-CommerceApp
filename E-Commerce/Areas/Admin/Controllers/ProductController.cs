using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;

        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var getAll = _db.Product.GetAll().ToList();
            return View(getAll);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product category)
        {
              if (ModelState.IsValid)
            {
                _db.Product.Add(category);
                _db.Save();
                TempData["Success"] = "Product created successfully";
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
            var category = _db.Product.Get(c => c.Id == Id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Product category)
        {

            if (ModelState.IsValid)
            {
                _db.Product.Update(category);
                _db.Save();
                TempData["Success"] = "Product Updated successfully";
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
            var category = _db.Product.Get(c => c.Id == Id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            var category = _db.Product.Get(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Product.Remove(category);
            _db.Save();
            TempData["Success"] = "Product Deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
