using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Core;
using System.Linq.Expressions;

namespace OnlineEvent.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class,IEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task ChangeStatusAsync(int id, bool status)
        {
            var value = await _dbSet.FindAsync(id);
            value.IsActive = status;
        }

        public virtual IQueryable<T> GetAll()
        {
            //AsNoTracking deme sebebimiz Memory almaması ve daha performanslı çalışması için
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
