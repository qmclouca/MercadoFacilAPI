using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> GetUserById(Guid id);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
