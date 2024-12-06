using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

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
            User toPostUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = user.Role
            };
            await _userRepository.AddAsync(toPostUser);
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
            if(user == null) return null;
            IEnumerable<UserAddress> userAddresses = await _userAddressRepository.GetAllAsync();
            IEnumerable<UserAddress> userAddressesFiltered = userAddresses.Where(ua => ua.UserId == id);                        
            List<Address> addresses = new List<Address>();
            foreach (UserAddress userAddress in userAddressesFiltered)
            {
                Address address = await _addressRepository.GetByIdAsync(userAddress.AddressId);
                
                addresses.Add(address);
            }
            user.Addresses = addresses;

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception e)
            {
            }
          
            return await GetUserById(user.Id);            
        }

        public async Task<IQueryable<User>> GetAllUsersQuery()
        {
            return await _userRepository.GetAllQueryAsync();
        }
    }
}
