using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
