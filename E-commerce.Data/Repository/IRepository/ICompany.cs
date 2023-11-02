using E_commerce.Models.Models;
using E_Commerce.Models;

namespace E_commerce.Data.Repository.IRepository
{
    public interface ICompany : IRepository<Company>
    {
        void Update(Company category); 
       
    }
}
