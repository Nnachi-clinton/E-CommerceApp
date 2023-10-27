using E_commerce.Data.Repository.IRepository;
using E_Commerce.Data;
using E_Commerce.Models;

namespace E_commerce.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {
        private AppDbContext _db;

        public CategoryRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Category category)
        {
           _db.Categories.Update(category);
        }
    }
}
