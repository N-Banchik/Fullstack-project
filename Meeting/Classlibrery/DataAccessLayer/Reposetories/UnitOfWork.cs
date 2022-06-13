using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.DataAccessLayer.IRepository;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer.Reposetories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
       
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public IAccountRepository _accountRepository { get; private set; }
        public UnitOfWork(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager,ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            
            _accountRepository = new AccountRepository( _context, _userManager, _signInManager, tokenService);
        }

       

        public async Task<bool> CompleteAsync()
        {
            //returns true if the items saved equal to the number of items have been changed.
            return await _context.SaveChangesAsync() == _context.ChangeTracker.Entries()
                                  .Where(x=>x.State!=EntityState.Unchanged&& x.State != EntityState.Detached).Count();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

