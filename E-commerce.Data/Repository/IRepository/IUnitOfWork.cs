namespace E_commerce.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        IProduct Product { get; }
        ICompany Company { get; }
        IShoppingCart ShoppingCart { get; }
        IUserRepository UserRepository { get; }
        void Save();


    }
}
