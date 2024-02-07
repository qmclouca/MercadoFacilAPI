using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        new Task<User> GetByEmailAsync(string email);
    }
}
