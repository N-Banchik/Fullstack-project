using AutoMapper;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repository.IRepository;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Reposetories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;



        public IAccountRepository _accountRepository { get; private set; }
        public IHobbyRepository _hobbyRepository { get; private set; }
        public ICategoryRepository _categoryRepository { get; private set; }
        public IGuideRepository _guideRepository { get; private set; }
        public IEventsRepository _eventsRepository { get; private set; }
        public IMemberRepository _memberRepository { get; private set; }
        public UnitOfWork(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _memberRepository = new MemberRepository(_context, userManager, photoService, mapper);
            _accountRepository = new AccountRepository(_context, userManager, signInManager, tokenService, mapper);
            _hobbyRepository = new HobbyRepository(_context, userManager, mapper, photoService);
            _categoryRepository = new CategoryRepository(_context, mapper);
            _guideRepository = new GuideRepository(_context, mapper, userManager);
            _eventsRepository = new EventsRepository(_context, mapper, userManager, photoService);

        }



        public async Task CompleteAsync()
        {

            await _context.SaveChangesAsync();

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

