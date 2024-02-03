using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;       

        public UserRepository(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        public Task AddAsync(User entity)
        {                     
           return _userRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return _userRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(User entity)
        {
            return _userRepository.DeleteAsync(entity);           
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(User entity)
        {
            return _userRepository.UpdateAsync(entity);
        }
    }
}