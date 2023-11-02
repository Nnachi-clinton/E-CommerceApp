namespace E_commerce.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        IProduct Product { get; }
        ICompany Company { get; }
        void Save();


    }
}
