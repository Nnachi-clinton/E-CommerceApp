namespace E_commerce.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        void Save();

    }
}
