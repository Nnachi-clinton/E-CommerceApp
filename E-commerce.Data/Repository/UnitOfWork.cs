using E_commerce.Data.Repository.IRepository;
using E_Commerce.Data;

namespace E_commerce.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategory Category { get; private set; }
        private AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
           _db.SaveChanges();       
        }
    }
}
