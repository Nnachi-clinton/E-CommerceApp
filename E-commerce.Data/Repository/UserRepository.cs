using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using E_Commerce.Data;
using E_Commerce.Models;

namespace E_commerce.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private AppDbContext _db;

        public UserRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

 
    }
}
