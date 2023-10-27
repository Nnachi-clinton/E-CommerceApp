using E_Commerce.Models;

namespace E_commerce.Data.Repository.IRepository
{
    public interface ICategory : IRepository<Category>
    {
        void Update(Category category); 
       
    }
}
