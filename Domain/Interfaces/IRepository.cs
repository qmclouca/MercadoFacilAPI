using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
        Task<IQueryable<T>> GetAllQueryAsync();
        Task<T> GetByEmailAsync(string email);
        Task<T> GetByEmailAsync(Expression<Func<T, bool>> value);
    }
}
