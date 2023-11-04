using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using E_Commerce.Data;
using E_Commerce.Models;

namespace E_commerce.Data.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCart
    {
        private AppDbContext _db;

        public ShoppingCartRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(ShoppingCart cart)
        {
           _db.ShoppingCarts.Update(cart);
        }
    }
}
