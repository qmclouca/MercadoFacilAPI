using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> AddUser(User user)
        {
            await _userRepository.AddAsync(user);
            return true;            
        }

        public async Task<bool> DeleteUser(User user)
        {
            if (user == null) return false;
            
            await _userRepository.DeleteAsync(user.Id);
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
            return await GetUserById(user.Id);            
        }
    }
}
