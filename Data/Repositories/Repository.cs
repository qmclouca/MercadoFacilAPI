using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MercadoFacilDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MercadoFacilDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
                return result;
            }
            catch (NullReferenceException e)
            {                
                return null;
            }
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null) return;
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();           
        }

        public Task<IQueryable<T>> GetAllQueryAsync()
        {
            return Task.FromResult<IQueryable<T>>(_dbSet);
        }

        public Task<T> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByEmailAsync(Expression<Func<T, bool>> value)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(value);
        }
    }
}