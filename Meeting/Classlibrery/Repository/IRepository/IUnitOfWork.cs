namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IAccountRepository _accountRepository { get; }
        public IHobbyRepository _hobbyRepository { get; }
        public ICategoryRepository _categoryRepository { get; }
        public IGuideRepository _guideRepository { get; }
        public IEventsRepository _eventsRepository { get; }
        public IMemberRepository _memberRepository { get; }

        Task CompleteAsync();
        void Dispose();
    }
}
