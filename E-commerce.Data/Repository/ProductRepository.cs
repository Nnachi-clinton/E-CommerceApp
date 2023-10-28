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
           _db.Products.Update(product);
        }

       
    }
}
