using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using E_commerce.Models.ViewModels;
using E_commerce.Utility;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _webEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webEnvironment)
        {
            _db = db;
            _webEnvironment = webEnvironment;   
        }

        public IActionResult Index()
        {
            var getAll = _db.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(getAll);
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModels productVm = new()
            {
                CategoryList = _db.Category.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new Product()
            };
            if (id==null || id==0)
            {
                //Create
                return View(productVm);
            }
            else
            {
                //Update
                productVm.Product = _db.Product.Get(x=>x.Id==id);
                return View(productVm);
            }

            
        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModels category, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath =Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(category.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, category.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    category.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (category.Product.Id==0)
                {
                    _db.Product.Add(category.Product);
                }
                else
                {
                    _db.Product.Update(category.Product);
                }

               
                _db.Save();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                category.CategoryList = _db.Category.GetAll()
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               });
                return View(category);
            }
           

        }
      
        //public IActionResult Delete(int Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _db.Product.Get(c => c.Id == Id);
        //    if (category == null)
        //    {
        //        return BadRequest();
        //    }
        //    return View(category);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int Id)
        //{
        //    var category = _db.Product.Get(x => x.Id == Id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Product.Remove(category);
        //    _db.Save();
        //    TempData["Success"] = "Product Deleted successfully";
        //    return RedirectToAction("Index");

        //}


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var getAll = _db.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new
            {
                data = getAll
            });
        } 
        
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var productToBeDeleted = _db.Product.Get(x => x.Id == Id);
            if (productToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            var oldImagePath =
                           Path.Combine(_webEnvironment.WebRootPath,
                           productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _db.Product.Remove(productToBeDeleted);
            _db.Save();

            return Json(new
            {
                success = true, message ="Deleted successfully"
            });
        }


        #endregion
    }
}
