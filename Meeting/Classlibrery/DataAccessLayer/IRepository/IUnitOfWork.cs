namespace Classlibrery.DataAccessLayer.IRepository
{
    public interface IUnitOfWork
    {
       public IAccountRepository _accountRepository { get; }
        
        Task<bool> CompleteAsync();
        void Dispose();
    }
}
