using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class UserAddressService: IUserAddressService
    {
        private readonly IRepository<UserAddress> _userAddressRepository;

        public UserAddressService(IRepository<UserAddress> userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        public async Task<bool> AddUserAddress(UserAddress userAddress)
        {
            await _userAddressRepository.AddAsync(userAddress);
            return true;
        }

        public async Task<bool> DeleteUserAddress(UserAddress userAddress)
        {
            await _userAddressRepository.DeleteAsync(userAddress);
            return true;
        }

        public async Task<IEnumerable<UserAddress>> GetAllUserAddresses()
        {
            return await _userAddressRepository.GetAllAsync();
        }

        public async Task<UserAddress> GetUserAddressById(Guid id)
        {
            return await _userAddressRepository.GetByIdAsync(id);
        }

        public async Task<UserAddress> UpdateUserAddress(UserAddress userAddress)
        {
            await _userAddressRepository.UpdateAsync(userAddress);
            return await GetUserAddressById(userAddress.Id);
        }
    }
}