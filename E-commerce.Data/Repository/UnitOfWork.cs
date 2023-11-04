using E_commerce.Data.Repository.IRepository;
using E_Commerce.Data;

namespace E_commerce.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategory Category { get; private set; }
        public IProduct Product { get; private set; }
        public ICompany Company { get; private set; }
        public IShoppingCart ShoppingCart { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        private AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            UserRepository = new UserRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
        }

        public void Save()
        {
           _db.SaveChanges();       
        }
    }
}
