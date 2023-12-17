using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
    }
}
