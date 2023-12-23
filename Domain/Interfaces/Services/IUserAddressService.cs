using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserAddressService
    {
        Task<bool> AddUserAddress(UserAddress userAddress);
        Task<UserAddress> UpdateUserAddress(UserAddress userAddress);
        Task<bool> DeleteUserAddress(UserAddress userAddress);
        Task<UserAddress> GetUserAddressById(Guid id);
        Task<IEnumerable<UserAddress>> GetAllUserAddresses();
    }
}
