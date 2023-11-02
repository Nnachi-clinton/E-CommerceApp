using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Companys
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _db;
        public CompanyController(IUnitOfWork db)
        {
            _db = db;
             
        }

        public IActionResult Index()
        {
            var getAll = _db.Company.GetAll().ToList();
            
            return View(getAll);
        }

        public IActionResult Upsert(int? id)
        {
            if (id==null || id==0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //Update
                Company company = _db.Company.Get(x=>x.Id==id);
                return View(company);
            }

            
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id==0)
                {
                    _db.Company.Add(company);
                }
                else
                {
                    _db.Company.Update(company);
                }

               
                _db.Save();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {             
                return View(company);
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
            var getAll = _db.Company.GetAll().ToList();
            return Json(new
            {
                data = getAll
            });
        } 
        
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var companyToBeDeleted = _db.Company.Get(x => x.Id == Id);
            if (companyToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
           _db.Company.Remove(companyToBeDeleted);
            _db.Save();

            return Json(new
            {
                success = true, message ="Deleted successfully"
            });
        }


        #endregion
    }
}
