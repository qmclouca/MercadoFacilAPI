using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
    public class SharesRepository: IShareRepository
    {
        private readonly IRepository<Share> _shareRepository;

        public SharesRepository(IRepository<Share> repository)
        {
            _shareRepository = repository;
        }

        public Task AddAsync(Share entity)
        {
            return _shareRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return _shareRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(Share entity)
        {
            return _shareRepository.DeleteAsync(entity);
        }

        public Task<IEnumerable<Share>> GetAllAsync()
        {
            return _shareRepository.GetAllAsync();
        }

        public Task<Share> GetByIdAsync(Guid id)
        {
            return _shareRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Share entity)
        {
            return _shareRepository.UpdateAsync(entity);
        }

        public async Task<IQueryable<Share>> GetAllSharesQuery()
        {
           return await _shareRepository.GetAllQueryAsync();
        }

        public Task<IQueryable<Share>> GetAllQueryAsync()
        {
            return _shareRepository.GetAllQueryAsync();
        }
    }
}
