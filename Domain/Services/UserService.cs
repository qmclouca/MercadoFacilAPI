using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public Task<User> CreateUserAsync(User user)
        {
            _repository.AddAsync(user);
            return Task.FromResult(user);
        }
    }
}
