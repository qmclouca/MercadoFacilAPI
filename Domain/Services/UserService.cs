using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using System.Text.Json;

namespace Domain.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<UserAddress> _userAddressRepository;

        public UserService(IRepository<User> userRepository, IRepository<Address> addressRepository, IRepository<UserAddress> userAddressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _userAddressRepository = userAddressRepository;
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
            User user = await _userRepository.GetByIdAsync(id);
            IEnumerable<UserAddress> userAddresses = _userAddressRepository.GetAllAsync().Result.Where(ua => ua.UserId == id);                        
            List<Address> addresses = new List<Address>();
            foreach (UserAddress userAddress in userAddresses)
            {
                Address address = await _addressRepository.GetByIdAsync(userAddress.AddressId);
                addresses.Add(address);
            } 
            
            user.Addresses = JsonSerializer.Serialize(addresses);

            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
            return await GetUserById(user.Id);            
        }
    }
}
