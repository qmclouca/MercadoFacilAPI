using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ShareService: IShareService
    {
        private readonly IRepository<Share> _shareRepository;
        public ShareService(IRepository<Share> shareRepository) 
        {
            _shareRepository = shareRepository;
        }

        public Task AddAsync(Share entity)
        {
            return _shareRepository.AddAsync(entity);
        }
        public Task DeleteAsync(Share entity)
        {
            return _shareRepository.DeleteAsync(entity);
        }
        public Task DeleteAsync(Guid id)
        {
            return _shareRepository.DeleteAsync(id);
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
    }
}
