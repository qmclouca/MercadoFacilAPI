using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _repository;       

        public UserRepository(IRepository<User> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(User entity)
        {                     
           return _repository.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task UpdateAsync(User entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}