using E_commerce.Models.Models;

namespace E_commerce.Data.Repository.IRepository
{
    public interface IProduct : IRepository<Product>
    {
        void Update(Product product); 
               
    }
}
