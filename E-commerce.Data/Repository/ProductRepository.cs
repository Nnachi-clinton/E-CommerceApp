using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using E_Commerce.Data;
using E_Commerce.Models;
using System.Linq.Expressions;

namespace E_commerce.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProduct
    {
        private AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }       

        public void Update(Product product)
        {
           var productfromDb = _db.Products.FirstOrDefault(u=>u.Id == product.Id);

            if (productfromDb != null)
            {
                productfromDb.Title = product.Title;
                productfromDb.ISBN = product.ISBN;
                productfromDb.Price = product.Price;
                productfromDb.Price50 = product.Price50;
                productfromDb.ListPrice = product.ListPrice;
                productfromDb.Price100 = product.Price100;
                productfromDb.Description = product.Description;
                productfromDb.CategoryId = product.CategoryId;
                productfromDb.Author = product.Author;
                if (productfromDb.ImageUrl != null)
                {
                    productfromDb.ImageUrl = product.ImageUrl;
                }
            }
        }

       
    }
}
