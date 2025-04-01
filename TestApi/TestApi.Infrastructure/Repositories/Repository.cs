using Microsoft.EntityFrameworkCore;
using TestApi.Domain.Interfaces.Repositories;

namespace TestApi.Infrastructure.Repositories
{
    public class Repository <TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TestApiDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(TestApiDbContext conetxt)
        {
            _context = conetxt;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

    }
}
