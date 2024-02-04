using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IShareService
    {
        Task AddAsync(Share share);       
        Task<Share> GetByIdAsync(Guid id);       
        Task<IEnumerable<Share>> GetAllAsync();
        Task<IQueryable<Share>> GetAllSharesQuery();
    }
}