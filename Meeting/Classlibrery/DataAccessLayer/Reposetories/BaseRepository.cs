using DataAccess.DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.DataAccessLayer.Reposetories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        internal DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;

            this._dbSet = _dbContext.Set<T>();

        }
        public virtual async void Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await this._dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await this._dbSet.FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual void Update(T entity)
        {
            this._dbSet.Update(entity);
        }

    }
}
