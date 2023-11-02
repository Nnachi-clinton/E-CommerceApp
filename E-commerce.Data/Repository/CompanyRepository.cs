using E_commerce.Data.Repository.IRepository;
using E_commerce.Models.Models;
using E_Commerce.Data;

namespace E_commerce.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompany
    {
        private AppDbContext _db;

        public CompanyRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Company company)
        {
           _db.Companies.Update(company);
        }
    }
}
